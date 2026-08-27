using UnityEngine;

/// <summary>
/// WeaponInputBinder / LookInputBinder와 동일한 패턴.
/// 플레이어 전용 - AI는 이 컴포넌트를 붙이지 않고 LockOnController의 공개 메서드를
/// (필요할 때만) 직접 호출하면 된다. 아무것도 호출하지 않아도 Auto 락온은 그대로 동작한다.
/// </summary>
[DisallowMultipleComponent]
public class LockonInputBinder : MonoBehaviour
{
    private ILockonInput _lockonInput;
    private ILookInput _lookInput;
    private LockonController _lockon;

    private void Awake()
    {
        _lockonInput = GetComponentInChildren<ILockonInput>();
        _lookInput = GetComponentInChildren<ILookInput>();
        _lockon = GetComponentInChildren<LockonController>();
    }

    private void OnEnable()
    {
        if (_lockonInput == null || _lookInput == null || _lockon == null)
        {
            Debug.Log($"ILockOnInput, ILookInput 혹은 LockOnController가 비어있습니다");
            return;
        }

        _lockonInput.LockOnManualToggleAction += _lockon.ToggleManualMode;
        _lockonInput.LockOnNextTargetAction += _lockon.SelectNextTarget;
        _lockonInput.LockOnPrevTargetAction += _lockon.SelectPrevTarget;

        // Look 입력(마우스 델타/스틱 값)을 Manual 조준 이동 + Auto 방향 타겟 전환 양쪽에 사용
        _lookInput.LookAction += _lockon.OnAimInput;
    }

    private void OnDisable()
    {
        if (_lockonInput == null || _lookInput == null || _lockon == null)
            return;

        _lockonInput.LockOnManualToggleAction -= _lockon.ToggleManualMode;
        _lockonInput.LockOnNextTargetAction -= _lockon.SelectNextTarget;
        _lockonInput.LockOnPrevTargetAction -= _lockon.SelectPrevTarget;

        _lookInput.LookAction -= _lockon.OnAimInput;
    }
}
