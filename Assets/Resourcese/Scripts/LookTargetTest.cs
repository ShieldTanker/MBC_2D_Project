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

        // 이번 프레임의 마우스 이동량 (화면 위치가 아님)
        Vector2 input = action.ReadValue<Vector2>();

        // 입력값만큼 현재 위치에서 이동
        Vector3 move = new Vector3(input.x, input.y, 0f) * sensitivity;
        Vector3 nextPosition = transform.position + move;

        // target을 중심으로 length 반경 안에만 위치하도록 제한
        Vector3 offset = nextPosition - target.position;
        offset = Vector3.ClampMagnitude(offset, length);

        transform.position = target.position + offset;
    }

    private void OnDrawGizmosSelected()
    {
        if (target == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.position, length);
    }
}