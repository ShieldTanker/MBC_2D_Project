using UnityEngine;
using UnityEngine.InputSystem;

public class LookTargetTest : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float length = 10f;
    [SerializeField] private float sensitivity = 0.01f;

    public void LookInput(Vector2 input)
    {
        if (target == null) return;

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