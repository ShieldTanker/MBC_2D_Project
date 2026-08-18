using UnityEngine;

public class BulletTest : MonoBehaviour
{
    Vector3 dir = Vector3.right;

    void Update()
    {
        transform.position += dir * 60f * Time.deltaTime;
    }
}
