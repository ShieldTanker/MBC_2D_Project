using UnityEngine;

public class GunTest : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        target.transform.position = transform.position;
        target.transform.rotation = transform.rotation;
    }
}
