using UnityEngine;

/// <summary>
/// WeaponInputBinder / LookInputBinder와 동일한 패턴.
/// 플레이어 전용 - AI는 이 컴포넌트를 붙이지 않고 LockOnController의 공개 메서드를
/// (필요할 때만) 직접 호출하면 된다. 아무것도 호출하지 않아도 Auto 락온은 그대로 동작한다.
/// </summary>
[DisallowMultipleComponent]
public class LockOnInputBinder : MonoBehaviour
{
    private ILockOnInput _lockOnInput;
    private ILookInput _lookInput;
    private LockOnController _lockOn;

    private void Awake()
    {
        _lockOnInput = GetComponent<ILockOnInput>();
        _lookInput = GetComponent<ILookInput>();
        _lockOn = GetComponent<LockOnController>();
    }

    private void OnEnable()
    {
        if (_lockOnInput == null || _lookInput == null || _lockOn == null)
        {
            Debug.Log($"ILockOnInput, ILookInput 혹은 LockOnController가 비어있습니다");
            return;
        }

        _lockOnInput.LockOnManualToggleAction += _lockOn.ToggleManualMode;
        _lockOnInput.LockOnNextTargetAction += _lockOn.SelectNextTarget;
        _lockOnInput.LockOnPrevTargetAction += _lockOn.SelectPrevTarget;

        // Look 입력(마우스 델타/스틱 값)을 Manual 조준 이동 + Auto 방향 타겟 전환 양쪽에 사용
        _lookInput.LookAction += _lockOn.OnAimInput;
    }

    private void OnDisable()
    {
        if (_lockOnInput == null || _lookInput == null || _lockOn == null)
            return;

        _lockOnInput.LockOnManualToggleAction -= _lockOn.ToggleManualMode;
        _lockOnInput.LockOnNextTargetAction -= _lockOn.SelectNextTarget;
        _lockOnInput.LockOnPrevTargetAction -= _lockOn.SelectPrevTarget;

        _lookInput.LookAction -= _lockOn.OnAimInput;
    }
}
