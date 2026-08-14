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
        _pInput.actions["Move"].performed += OnMoveInput;
        _pInput.actions["Move"].canceled += OnMoveInput;
        _pInput.actions["Jump"].performed += OnJumpInput;
        _pInput.actions["Jump"].canceled += OnJumpInput;

        _pInput.actions["Fire1"].performed += OnFHandFire;
        _pInput.actions["Fire2"].performed += OnBHandFire;
        _pInput.actions["Fire3"].performed += OnFShoulderFire;
        _pInput.actions["Fire4"].performed += OnBShoulderFire;
    }

    private void OnDisable()
    {
        _pInput.actions["Move"].performed -= OnMoveInput;
        _pInput.actions["Move"].canceled -= OnMoveInput;
        _pInput.actions["Jump"].performed -= OnJumpInput;
        _pInput.actions["Jump"].canceled -= OnJumpInput;

        _pInput.actions["Fire1"].performed -= OnFHandFire;
        _pInput.actions["Fire2"].performed -= OnBHandFire;
        _pInput.actions["Fire3"].performed -= OnFShoulderFire;
        _pInput.actions["Fire4"].performed -= OnBShoulderFire;
    }

    // 이동 입력
    void OnMoveInput(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();

    void OnJumpInput(InputAction.CallbackContext context) => JumpInput = context.ReadValueAsButton();
    

    // 무기 사격 입력
    void OnFHandFire(InputAction.CallbackContext context) => F_HandFire?.Invoke();

    void OnBHandFire(InputAction.CallbackContext context) => B_HandFire?.Invoke();

    void OnFShoulderFire(InputAction.CallbackContext context) => F_ShoulderFire?.Invoke();

    void OnBShoulderFire(InputAction.CallbackContext context) => B_ShoulderFire?.Invoke();
}