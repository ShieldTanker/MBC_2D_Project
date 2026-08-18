using UnityEngine;

public class BulletTest : MonoBehaviour
{
    private Transform _target;

    private Vector3 dir = Vector3.right;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += dir * 60f * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        if(target == null) return;
        _target = target;

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
