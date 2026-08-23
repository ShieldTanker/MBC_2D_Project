using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 락온의 핵심 컨트롤러.
/// - 공통: 실제 추적 위치는 상태와 무관하게 매 프레임 예측 위치를 AgentStat.LockOnSpeed 만큼 프레임 독립적으로 따라간다.
/// range / lockOnSpeed / manualAimSpeed는 전부 AgentStat(= 파츠 스탯 합산 결과)에서 읽으므로
/// 파츠를 교체해 LoadOut.RecalculateStats()가 실행되면 다음 프레임부터 자동으로 반영된다.
///
/// 입력은 이 컨트롤러를 전혀 몰라도 된다 
/// - ToggleManualMode, SelectNextTarget, SelectPrevTarget, OnAimInput은 LockOnInputBinder(플레이어) 또는 AI 로직이 직접 호출하는 공개 API다.
/// </summary>
[DisallowMultipleComponent]
public class LockonController : MonoBehaviour
{
    [Header("스탯 소스 (감지거리 / 락온속도 / 수동조작속도)")]
    [SerializeField] AgentStat _agentStat;
    [SerializeField] Camera _viewCamera;

    [Header("기술 튜닝 (마스크, LOS, 화면범위 등 - 파츠와 무관)")]
    [SerializeField] LockOnTuning _tuning;

    [Header("무기에 전달할 위치")]
    [Tooltip("실제 추적 위치")]
    [SerializeField] Transform _trackingTargetTransform;

    [Tooltip("예측 위치 (선택). 락온 UI 마커 등에서 참고용으로만 사용")]
    [SerializeField] Transform _predictedTargetTransform;

    [Header("진영 (아군 오인 락온 방지)")]
    [Tooltip("자기 진영. 같은 오브젝트에 ITargetable(TargetableEntity 등)이 있으면 그 Faction을 자동으로 가져와 덮어쓴다. 없으면 이 값을 그대로 사용한다.")]
    [SerializeField] Faction selfFaction = Faction.None;

    LockonMode _lockonMode = LockonMode.Auto;

    ITargetSelector _selector;

    ITargetable _currentTarget;
    List<ITargetable> _targetList = new List<ITargetable>();
    int _targetIndex;

    /// <summary> 예측 트랜스폼의 포지션 </summary>
    Vector3 _predictedPosition;
    /// <summary> 추적하는 트랜스폼의 포지션 </summary>
    Vector3 _trackingPosition;

    [Header("예측 관련")]
    [Tooltip("예측 사용 유무")]
    public bool UsePredicted;
    [Tooltip("예측 시간")]
    public float PredictedLeadTime;

    [Header("디버그")]
    [SerializeField] float _trackingTargetRadius;
    [SerializeField] float _predictedTargetRadius;

    #region 외부 공개용 속성들
    /// <summary> 현재 락온이 무슨 모드인지 반환 </summary>
    public LockonMode CurrentMode => _lockonMode;
    /// <summary> 현재 타겟 인터페이스를 반환 </summary>
    public ITargetable CurrentTarget => _currentTarget;

    public Vector3 PredictedPosition => _predictedPosition;

    public Vector3 TrackingPosition => _trackingPosition;
    public IReadOnlyList<ITargetable> Targets => _targetList;

    public Transform TrackingTargetTransform => _trackingTargetTransform;
    public Transform CurrentTargetTransform => _currentTarget?.TargetTransform;
    #endregion

    #region 이벤트들
    /// <summary> 타겟이 추가될때 액션 </summary>
    public event Action<ITargetable> OnTargetAcquired;
    /// <summary> 현재 타겟이 사라질때 액션 </summary>
    public event Action<ITargetable> OnTargetLost;
    public event Action<LockonMode> OnModeChanged;
    #endregion

    void Update()
    {
        if (_agentStat == null || _tuning == null)
            return;

        if (_lockonMode == LockonMode.Auto)
            TickAuto();

        SetPosition();
    }

    public void Init(AgentStat stat, ITargetSelector selector)
    {
        CreatePosition();
        _predictedPosition = transform.position;
        _trackingPosition = transform.position;

        _selector = selector;
        _agentStat = stat;

        // 자기 자신도 ITargetable(적에게 보이는 대상)이라면 그쪽 Faction을 신뢰할 수 있는 값으로 사용
        var selfTargetable = GetComponent<ITargetable>();

        if (selfTargetable != null)
            selfFaction = selfTargetable.Faction;
    }

    void CreatePosition()
    {
        if (_predictedTargetTransform == null)
        {
            GameObject go = new GameObject("PredictedTarget");
            go.transform.parent = transform;
            _predictedTargetTransform = go.transform;
        }

        if (_trackingTargetTransform == null)
        {
            GameObject go = new GameObject("TrackingTarget");
            go.transform.parent = transform;
            _trackingTargetTransform = go.transform;
        }
    }

    void SetPosition()
    {
        // 프레임 독립적 지수 감쇠 추적
        float t = 1f - Mathf.Exp(-_agentStat.LockOnSpeed * Time.deltaTime);

        // 추격 포지션을 예측 포지션으로 선형보간
        _trackingPosition = Vector3.Lerp(_trackingPosition, _predictedPosition, t);

        if (_trackingTargetTransform != null)
            _trackingTargetTransform.position = _trackingPosition;

        if (_predictedTargetTransform != null)
            _predictedTargetTransform.position = _predictedPosition;
    }

    void TickAuto()
    {
        _targetList = _selector.GetTargets(transform, _agentStat.LockonRange, _tuning, _viewCamera, selfFaction, _currentTarget);

        if (_currentTarget != null)
        {
            if (!_currentTarget.IsLockable || !_targetList.Contains(_currentTarget)) ClearTarget();
        }

        // 대상이 없다가 범위 내에 대상이 생기면 자동으로 락온
        if (_currentTarget == null && _targetList.Count > 0)
            SetCurrentTarget(_targetList[0]);

        if (_currentTarget != null)
        {
            _predictedPosition = UsePredicted ? CalculatePredictedPosition(PredictedLeadTime) : _currentTarget.Position;
        }
        else
        {
            ClampPredictedPositionToRange();
        }
    }

    /// <summary>
    /// 예측위치 계산
    /// </summary>
    /// <param name="leadTime">예상 시간</param>
    /// <returns></returns>
    Vector3 CalculatePredictedPosition(float leadTime) => _currentTarget.Position + _currentTarget.Velocity * leadTime;
    
    /// <summary>
    /// 대상이 없으면 OnAimInput에서 예측 위치를 수동처럼 직접 이동시키고, 
    /// <br>매 프레임 여기서 감지범위 밖으로 못 나가게 클램프한다.</br>
    /// <br>(마지막으로 타겟을 쫓던 방향은 유지한 채 최대 감지거리 안으로 당겨온다)</br>
    /// </summary>
    void ClampPredictedPositionToRange()
    {
        Vector3 offset = _predictedPosition - transform.position;
        _predictedPosition = transform.position + Vector3.ClampMagnitude(offset, _agentStat.LockonRange);
    }

    void SetCurrentTarget(ITargetable target)
    {
        if (_currentTarget == target)
            return;

        _currentTarget = target;
        _targetIndex = _targetList.IndexOf(target);

        OnTargetAcquired?.Invoke(target);
    }

    void ClearTarget()
    {
        if (_currentTarget == null) return;

        OnTargetLost?.Invoke(_currentTarget);
        _currentTarget = null;
    }

    #region ------------------------------------ 입력 바인딩 대상 공개 API ------------------------------------
    /// <summary> Auto/Manual 모드 전환. </summary>
    public void ToggleManualMode()
    {
        _lockonMode = _lockonMode == LockonMode.Auto ? LockonMode.Manual : LockonMode.Auto;

        // Manual로 전환하면 범위 내 대상이 있어도 무시해야 하므로 즉시 타겟 연결 해제
        if (_lockonMode == LockonMode.Manual)
            ClearTarget();

        OnModeChanged?.Invoke(_lockonMode);
    }

    /// <summary>
    /// 연속적인 조준/방향 입력
    /// <br>Manual 모드이거나 Auto 모드에서 현재 타겟이 없을 때만 예측 위치를 직접 이동시킨다.</br>
    /// <br>타겟이 있는 동안은 TickAuto가 매 프레임 예측 위치를 덮어쓰므로 이 입력은 무시된다</br>
    /// </summary>
    public void OnAimInput(Vector2 value)
    {
        if(_agentStat == null)
            return;
        // Manual 모드 or 현재 락온된 대상이 없으면 수동 조준과 동일하게 예측 위치를 직접 이동시킨다.
        // 대상이 있는 동안은 TickAuto가 매 프레임 _predictedPosition을 대상 위치로 덮어쓰므로 여기서 이동시켜도 다음 프레임에 무시된다.
        if (_lockonMode == LockonMode.Manual || (_lockonMode == LockonMode.Auto && _currentTarget == null))
        {
            Vector3 move = new Vector3(value.x, value.y, 0f) * _agentStat.ManualAimSpeed * Time.deltaTime;
            _predictedPosition += move;
            ClampPredictedPositionToRange();
            return;
        }
    }

    /// <summary> 다음 타겟으로 이산적 전환 - 키/버튼에 바인딩해서 사용. </summary>
    public void SelectNextTarget()
    {
        if (_lockonMode != LockonMode.Auto || _targetList.Count == 0)
            return;

        _targetIndex = (_targetIndex + 1) % _targetList.Count;
        SetCurrentTarget(_targetList[_targetIndex]);
    }

    /// <summary> 이전 타겟으로 이산적 전환 - 키/버튼에 바인딩해서 사용. </summary>
    public void SelectPrevTarget()
    {
        if (_lockonMode != LockonMode.Auto || _targetList.Count == 0)
            return;

        _targetIndex = (_targetIndex - 1 + _targetList.Count) % _targetList.Count;
        SetCurrentTarget(_targetList[_targetIndex]);
    }

    #endregion ------------------------------------------------------------------------

    /// <summary> 정렬 기준을 바꾸고 싶을 때 다른 ITargetSelector 구현체로 교체.</summary>
    public void SetSelector(ITargetSelector selector) => _selector = selector;

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (_agentStat == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _agentStat.LockonRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_predictedPosition, _predictedTargetRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_trackingPosition, _trackingTargetRadius);
    }
#endif
}