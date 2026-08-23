using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : InputController
{
    private PlayerInput _pInput;

    private void Awake()
    {
        _pInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _pInput.actions["Move"].performed += OnMoveInputPerformed;
        _pInput.actions["Jump"].performed += OnJumpInputPerformed;
        _pInput.actions["Move"].canceled += OnMoveInputPerformed;
        _pInput.actions["Jump"].canceled += OnJumpInputCanceled;

        _pInput.actions["Look"].performed += OnLookInputPerformed;

        _pInput.actions["Fire1"].performed += OnBHandPerformedFire;
        _pInput.actions["Fire2"].performed += OnFHandPerformedFire;
        _pInput.actions["Fire3"].performed += OnBShoulderPerformedFire;
        _pInput.actions["Fire4"].performed += OnFShoulderPerformedFire;

        _pInput.actions["Fire1"].canceled += OnBHandCanceledFire;
        _pInput.actions["Fire2"].canceled += OnFHandCanceledFire;
        _pInput.actions["Fire3"].canceled += OnBShoulderCanceledFire;
        _pInput.actions["Fire4"].canceled += OnFShoulderCanceledFire;

        // 락온 입력 - Input Actions 에셋에 "LockOnToggle" / "LockOnNext" / "LockOnPrev" 액션을 추가해야 함
        _pInput.actions["LockOnToggle"].performed += OnLockOnManualTogglePerformed;
        _pInput.actions["LockOnNext"].performed += OnLockOnNextTargetPerformed;
        _pInput.actions["LockOnPrev"].performed += OnLockOnPrevTargetPerformed;
    }

    private void OnDisable()
    {
        _pInput.actions["Move"].performed -= OnMoveInputPerformed;
        _pInput.actions["Move"].canceled -= OnMoveInputPerformed;
        _pInput.actions["Jump"].performed -= OnJumpInputPerformed;
        _pInput.actions["Jump"].canceled -= OnJumpInputCanceled;

        _pInput.actions["Fire1"].performed -= OnBHandPerformedFire;
        _pInput.actions["Fire2"].performed -= OnFHandPerformedFire;
        _pInput.actions["Fire3"].performed -= OnBShoulderPerformedFire;
        _pInput.actions["Fire4"].performed -= OnFShoulderPerformedFire;

        _pInput.actions["Fire1"].canceled -= OnBHandCanceledFire;
        _pInput.actions["Fire2"].canceled -= OnFHandCanceledFire;
        _pInput.actions["Fire3"].canceled -= OnBShoulderCanceledFire;
        _pInput.actions["Fire4"].canceled -= OnFShoulderCanceledFire;

        _pInput.actions["LockOnToggle"].performed -= OnLockOnManualTogglePerformed;
        _pInput.actions["LockOnNext"].performed -= OnLockOnNextTargetPerformed;
        _pInput.actions["LockOnPrev"].performed -= OnLockOnPrevTargetPerformed;
    }

    // 이동 입력
    void OnMoveInputPerformed(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<float>();
        MoveInput = new Vector2(Horizontal, 0);
    }

    void OnJumpInputPerformed(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValueAsButton();
        JumpPressed = true;
    }

    void OnJumpInputCanceled(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValueAsButton();
    }

    // 시점 입력
    void OnLookInputPerformed(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
        LookAction?.Invoke(LookInput);
    }

    // 무기 사격 입력
    void OnFHandPerformedFire(InputAction.CallbackContext context) => F_HandPerformedFire?.Invoke();

    void OnBHandPerformedFire(InputAction.CallbackContext context) => B_HandPerformedFire?.Invoke();

    void OnFShoulderPerformedFire(InputAction.CallbackContext context) => F_ShoulderPerformedFire?.Invoke();

    void OnBShoulderPerformedFire(InputAction.CallbackContext context) => B_ShoulderPerformedFire?.Invoke();
    

    // 무기 사격 입력 해제
    void OnFHandCanceledFire(InputAction.CallbackContext context) => F_HandCanceledFire?.Invoke();

    void OnBHandCanceledFire(InputAction.CallbackContext context) => B_HandCanceledFire?.Invoke();

    void OnFShoulderCanceledFire(InputAction.CallbackContext context) => F_ShoulderCanceledFire?.Invoke();

    void OnBShoulderCanceledFire(InputAction.CallbackContext context) => B_ShoulderCanceledFire?.Invoke();

    // 락온 입력
    void OnLockOnManualTogglePerformed(InputAction.CallbackContext context) => LockOnManualToggleAction?.Invoke();

    void OnLockOnNextTargetPerformed(InputAction.CallbackContext context) => LockOnNextTargetAction?.Invoke();

    void OnLockOnPrevTargetPerformed(InputAction.CallbackContext context) => LockOnPrevTargetAction?.Invoke();
}
