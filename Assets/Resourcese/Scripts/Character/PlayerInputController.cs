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
        _pInput.actions["Jump"].canceled += OnJumpInputPerformed;

        _pInput.actions["Look"].performed += OnLookInputPerformed;

        _pInput.actions["Fire1"].performed += OnFHandPerformedFire;
        _pInput.actions["Fire2"].performed += OnBHandPerformedFire;
        _pInput.actions["Fire3"].performed += OnFShoulderPerformedFire;
        _pInput.actions["Fire4"].performed += OnBShoulderPerformedFire;

        _pInput.actions["Fire1"].canceled += OnFHandCanceledFire;
        _pInput.actions["Fire2"].canceled += OnBHandCanceledFire;
        _pInput.actions["Fire3"].canceled += OnFShoulderCanceledFire;
        _pInput.actions["Fire4"].canceled += OnBShoulderCanceledFire;
    }

    private void OnDisable()
    {
        _pInput.actions["Move"].performed -= OnMoveInputPerformed;
        _pInput.actions["Move"].canceled -= OnMoveInputPerformed;
        _pInput.actions["Jump"].performed -= OnJumpInputPerformed;
        _pInput.actions["Jump"].canceled -= OnJumpInputPerformed;

        _pInput.actions["Fire1"].performed -= OnFHandPerformedFire;
        _pInput.actions["Fire2"].performed -= OnBHandPerformedFire;
        _pInput.actions["Fire3"].performed -= OnFShoulderPerformedFire;
        _pInput.actions["Fire4"].performed -= OnBShoulderPerformedFire;

        _pInput.actions["Fire1"].canceled -= OnFHandCanceledFire;
        _pInput.actions["Fire2"].canceled -= OnBHandCanceledFire;
        _pInput.actions["Fire3"].canceled -= OnFShoulderCanceledFire;
        _pInput.actions["Fire4"].canceled -= OnBShoulderCanceledFire;
    }

    // 이동 입력
    void OnMoveInputPerformed(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();

    void OnJumpInputPerformed(InputAction.CallbackContext context) => JumpInput = context.ReadValueAsButton();
    
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
}