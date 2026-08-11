using UnityEngine;
using UnityEngine.InputSystem;

public class LookTargetTest : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float length = 10f;
    [SerializeField] private float sensitivity = 0.01f;

    // Binding: <Mouse>/delta
    [SerializeField] private InputAction action;

    private void OnEnable()
    {
        action?.Enable();
    }

    private void OnDisable()
    {
        action?.Disable();
    }

    private void Update()
    {
        if (target == null || action == null)
            return;

        Vector2 input = action.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, input.y, 0f) * sensitivity;
        Vector3 desire = transform.position + move;

        Vector3 dest = desire - target.position;
        dest = Vector3.ClampMagnitude(dest, length);

        transform.position = target.position + dest;
    }

    private void OnDrawGizmosSelected()
    {
        if (target == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.position, length);
    }
}