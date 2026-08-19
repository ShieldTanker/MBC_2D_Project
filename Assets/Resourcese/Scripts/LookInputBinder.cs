using UnityEngine;

public class LookInputBinder : MonoBehaviour
{
    ILookInput _lookInput;
    LookTargetTest _lookTarget;

    private void Awake()
    {
        _lookInput = GetComponentInChildren<ILookInput>();
        _lookTarget = GetComponentInChildren<LookTargetTest>();
    }

    private void OnEnable()
    {
        _lookInput.LookAction += _lookTarget.LookInput;
    }

    private void OnDisable()
    {
        _lookInput.LookAction -= _lookTarget.LookInput;
    }
}
