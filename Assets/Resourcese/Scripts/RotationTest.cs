using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public GameObject destination;
    float angle;
    public float speed;

    void Update()
    {
        Vector3 dir = destination.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
