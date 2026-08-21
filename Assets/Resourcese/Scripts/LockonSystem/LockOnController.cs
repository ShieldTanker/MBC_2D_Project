using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 락온의 핵심 컨트롤러.
///
/// - Auto 상태: 범위 내 대상이 있으면 예측 위치 = 대상 위치(또는 리드 예측 위치).
///              대상이 없으면 Manual처럼 조준 입력으로 예측 위치를 자유롭게 움직일 수 있고,
///              그 상태에서 범위 내에 후보가 다시 나타나면 가장 가까운 대상으로 자동 락온한다.
/// - Manual 상태: ApplyManualAimDelta로 예측 위치를 직접 이동. 대상이 있어도 무시한다.
/// - 공통: 실제 추적 위치는 상태와 무관하게 매 프레임 예측 위치를
///        AgentStat.LockOnSpeed 만큼 프레임 독립적으로 따라간다.
///
/// range / lockOnSpeed / manualAimSpeed는 전부 AgentStat(= 파츠 스탯 합산 결과)에서 읽으므로
/// 파츠를 교체해 LoadOut.RecalculateStats()가 실행되면 다음 프레임부터 자동으로 반영된다.
///
/// 입력은 이 컨트롤러를 전혀 몰라도 된다 - ToggleManualMode / SelectNextTarget /
/// SelectPrevTarget / ApplyManualAimDelta는 LockOnInputBinder(플레이어) 또는
/// AI 로직이 직접 호출하는 공개 API다. AI는 아무것도 호출하지 않아도 Auto 상태가
/// 기본값이므로 자동 락온이 그대로 동작한다.
/// </summary>
[DisallowMultipleComponent]
public class LockOnController : MonoBehaviour
{
    public enum Mode { Auto, Manual }

    [Header("스탯 소스 (감지거리 / 락온속도 / 수동조작속도)")]
    [SerializeField] AgentStat agentStat;

    [Header("기술 튜닝 (마스크, LOS, 화면범위 등 - 파츠와 무관)")]
    [SerializeField] LockOnTuning tuning;

    [SerializeField] Camera viewCamera;

    [Header("무기에 전달할 위치")]
    [Tooltip("실제 추적 위치. 각 Weapon의 AimTarget 필드에 이 Transform을 꽂아주면 됨")]
    [SerializeField] Transform trackingTargetTransform;

    [Tooltip("예측 위치 (선택). 락온 UI 마커 등에서 참고용으로만 사용")]
    [SerializeField] Transform predictedTargetTransform;

    [Header("진영 (아군 오인 락온 방지)")]
    [Tooltip("자기 진영. 같은 오브젝트에 ITargetable(TargetableEntity 등)이 있으면 그 Faction을 자동으로 가져와 덮어쓴다. 없으면 이 값을 그대로 사용한다.")]
    [SerializeField] Faction selfFaction = Faction.None;

    ITargetSelector _selector;

    Mode _mode = Mode.Auto;
    ITargetable _currentTarget;
    List<ITargetable> _candidates = new List<ITargetable>();
    int _candidateIndex;

    Vector3 _predictedPosition;
    Vector3 _trackingPosition;

    Vector2 _aimInputAccum;
    float _targetSwitchCooldownTimer;

    public Mode CurrentMode => _mode;
    public ITargetable CurrentTarget => _currentTarget;
    public Vector3 PredictedPosition => _predictedPosition;
    public Vector3 TrackingPosition => _trackingPosition;
    public IReadOnlyList<ITargetable> Candidates => _candidates;

    #region 이벤트들
    public event Action<ITargetable> OnTargetAcquired;
    public event Action<ITargetable> OnTargetLost;
    public event Action<Mode> OnModeChanged;
    #endregion

    void Awake()
    {
        _predictedPosition = transform.position;
        _trackingPosition = transform.position;

        _selector ??= new DistanceTargetSelector();

        if (agentStat == null)
            agentStat = GetComponent<AgentStat>();

        // 자기 자신도 ITargetable(적에게 보이는 대상)이라면 그쪽 Faction을 신뢰할 수 있는 값으로 사용
        var selfTargetable = GetComponent<ITargetable>();
        if (selfTargetable != null)
            selfFaction = selfTargetable.Faction;
    }

    /// <summary>정렬 기준을 바꾸고 싶을 때 다른 ITargetSelector 구현체로 교체.</summary>
    public void SetSelector(ITargetSelector selector) => _selector = selector;

    void Update()
    {
        if (agentStat == null || tuning == null) return;
        Tick(Time.deltaTime);
    }

    void Tick(float dt)
    {
        if (_mode == Mode.Auto)
            TickAuto(dt);

        // 프레임 독립적 지수 감쇠 추적
        float t = 1f - Mathf.Exp(-agentStat.LockOnSpeed * dt);
        _trackingPosition = Vector3.Lerp(_trackingPosition, _predictedPosition, t);

        if (trackingTargetTransform != null)
            trackingTargetTransform.position = _trackingPosition;

        if (predictedTargetTransform != null)
            predictedTargetTransform.position = _predictedPosition;
    }

    #region 입력 바인딩 대상 공개 API (LockOnInputBinder 또는 AI 로직이 직접 호출)

    /// <summary>Auto/Manual 모드 전환.</summary>
    public void ToggleManualMode()
    {
        Mode newMode = _mode == Mode.Auto ? Mode.Manual : Mode.Auto;

        // Manual로 전환하면 범위 내 대상이 있어도 무시해야 하므로 즉시 타겟 연결 해제
        if (newMode == Mode.Manual)
            ClearTarget();

        _mode = newMode;
        OnModeChanged?.Invoke(_mode);
    }

    /// <summary>
    /// 연속적인 조준/방향 입력 (기존 ILookInput.LookAction과 동일한 시그니처).
    /// Manual 모드에서는 예측 위치를 직접 이동시키고, Auto 모드에서는 값을 누적해뒀다가
    /// TickAuto에서 누적치가 임계값을 넘는 순간(=의도적인 플릭 제스처)에만 방향 전환에 사용한다.
    /// 프레임 단위 순간값을 그대로 비교하면 미세한 떨림에도 계속 전환돼버리므로 누적 방식을 쓴다.
    /// </summary>
    public void OnAimInput(Vector2 value)
    {
        // Manual 모드이거나, Auto 모드인데 현재 락온된 대상이 없으면(=고정할 위치가 없으면)
        // 수동 조준과 동일하게 예측 위치를 직접 이동시킨다.
        // 대상이 있는 동안은 TickAuto가 매 프레임 _predictedPosition을 대상 위치로 덮어쓰므로
        // 여기서 이동시켜도 다음 프레임에 무시된다.
        if (_mode == Mode.Manual || (_mode == Mode.Auto && _currentTarget == null))
        {
            Vector3 move = new Vector3(value.x, value.y, 0f) * agentStat.ManualAimSpeed * Time.deltaTime;
            _predictedPosition += move;
            ClampPredictedPositionToRange();
            return;
        }

        _aimInputAccum += value;
    }

    /// <summary>
    /// 방향 기반 전환의 대안으로 남겨둔 이산적 전환 - 필요하면 키/버튼에 그대로 바인딩해서 쓸 수 있다.
    /// </summary>
    public void SelectNextTarget()
    {
        if (_mode != Mode.Auto || _candidates.Count == 0) return;
        _candidateIndex = (_candidateIndex + 1) % _candidates.Count;
        SetTarget(_candidates[_candidateIndex]);
    }

    public void SelectPrevTarget()
    {
        if (_mode != Mode.Auto || _candidates.Count == 0) return;
        _candidateIndex = (_candidateIndex - 1 + _candidates.Count) % _candidates.Count;
        SetTarget(_candidates[_candidateIndex]);
    }

    #endregion

    void TickAuto(float dt)
    {
        _candidates = _selector.GetCandidates
            (transform, agentStat.LockOnRange, tuning, viewCamera, selfFaction, _currentTarget);

        if (_currentTarget != null)
        {
            if (!_currentTarget.IsLockable || !_candidates.Contains(_currentTarget))
                ClearTarget();
        }

        // 대상이 없다가 범위 내에 대상이 생기면 자동으로 락온
        if (_currentTarget == null && _candidates.Count > 0)
            SetTarget(_candidates[0]);

        if (_targetSwitchCooldownTimer > 0f)
            _targetSwitchCooldownTimer -= dt;

        // 입력이 없는 동안 누적치를 서서히 0으로 감쇠 - 여러 프레임의 잔떨림이 쌓이는 것을 방지
        _aimInputAccum = Vector2.MoveTowards(_aimInputAccum, Vector2.zero, tuning.aimAccumDecayPerSecond * dt);

        if (_currentTarget != null)
        {
            _predictedPosition = tuning.useLeadPrediction
                ? _currentTarget.Position + _currentTarget.Velocity * tuning.leadTime
                : _currentTarget.Position;
        }
        else
        {
            // 대상이 없으면 OnAimInput에서 예측 위치를 수동처럼 직접 이동시키고,
            // 매 프레임 여기서 감지범위 밖으로 못 나가게 클램프한다.
            // (마지막으로 타겟을 쫓던 방향은 유지한 채 최대 감지거리 안으로 당겨온다)
            ClampPredictedPositionToRange();
        }
    }

    void ClampPredictedPositionToRange()
    {
        Vector3 offset = _predictedPosition - transform.position;
        _predictedPosition = transform.position + Vector3.ClampMagnitude(offset, agentStat.LockOnRange);
    }

    void SetTarget(ITargetable target)
    {
        if (_currentTarget == target)
            return;
        _currentTarget = target;
        _candidateIndex = _candidates.IndexOf(target);
        OnTargetAcquired?.Invoke(target);
    }

    public Transform GetTrackingTarget()
    {
        return trackingTargetTransform;
    }

    public Transform GetCurrentTarget()
    {
        return _currentTarget?.TargetTransform;
    }

    void ClearTarget()
    {
        if (_currentTarget == null) return;
        var lost = _currentTarget;
        _currentTarget = null;
        OnTargetLost?.Invoke(lost);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (agentStat == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agentStat.LockOnRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_predictedPosition, 0.2f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_trackingPosition, 0.15f);
    }
#endif
}