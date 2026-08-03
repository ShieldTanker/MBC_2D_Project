using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, IMoveInput2D, IJumpInput
{
    public Vector2 MoveInput {  get; private set; }

    public bool JumpInput {  get; private set; }

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
    }

    private void OnDisable()
    {
        _pInput.actions["Move"].performed -= OnMoveInput;
        _pInput.actions["Move"].canceled -= OnMoveInput;
        _pInput.actions["Jump"].performed -= OnJumpInput;
        _pInput.actions["Jump"].canceled -= OnJumpInput;
    }

    void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValueAsButton();
    }
}
