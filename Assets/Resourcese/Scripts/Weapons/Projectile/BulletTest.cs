using UnityEngine;

public class BulletTest : MonoBehaviour
{
    protected float _time = 0f;
    public Transform _target;
    public Transform _followTarget;

    protected float _bulletSpeed = 1f;
    protected float _maxRotateAngle = 30f;
    protected float _rotateSpeed = 10;

    protected Vector3 dir = Vector3.right;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        _time += Time.deltaTime;
        Move();
    }

    public virtual void Move()
    {
        transform.position += dir * _bulletSpeed * Time.deltaTime;
    }

    public void SetData(WeaponData data)
    {
        _bulletSpeed = data.BulletSpeed;
        _maxRotateAngle = data.MaxRotateAngle;
        _rotateSpeed = data.MaxRotateSpeed;
    }

    public void SetTarget(Transform target)
    {
        if (target == null) return;
        _target = target;
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetFollowTarget(Transform followTarget)
    {
        if(followTarget == null) return;
        _followTarget = followTarget;
    }
}
