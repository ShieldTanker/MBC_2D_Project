using UnityEngine;

public class BulletTest : MonoBehaviour
{
    protected float _time = 0f;
    public Transform _target;
    public Transform _followTarget;

    protected int _damage = 0;
    protected float _bulletSpeed = 1f;
    protected float _maxRotateAngle = 30f;
    protected float _rotateSpeed = 10;

    protected Vector3 _dir = Vector3.right;

    protected Vector3 _firePosition;
    protected float _fireAngle;
    protected float _currentAngle;

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
        transform.position += _dir * _bulletSpeed * Time.deltaTime;
    }

    public void SetData(WeaponData data)
    {
        _bulletSpeed = data.BulletSpeed;
        _maxRotateAngle = data.MaxRotateAngle;
        _rotateSpeed = data.MaxRotateSpeed;
    }

    /// <summary>
    /// 사격지점(위치/회전)을 세팅한다. 타겟이 있으면 발사각(_fireAngle) 기준
    /// ±_maxRotateAngle 범위 내로 클램프된 각도까지 즉시 회전시킨다.
    /// </summary>
    public void SetFireTrasnform(Transform fireTransform)
    {
        _firePosition = fireTransform.position;
        _fireAngle = fireTransform.rotation.eulerAngles.z;

        transform.position = fireTransform.position;
        transform.rotation = fireTransform.rotation;

        _currentAngle = _fireAngle;

        if (_target != null)
        {
            Vector3 toTarget = _target.position - transform.position;
            if (toTarget.sqrMagnitude > 0.0001f)
            {
                float targetAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
                float angleFromFire = Mathf.DeltaAngle(_fireAngle, targetAngle);
                angleFromFire = Mathf.Clamp(angleFromFire, -_maxRotateAngle, _maxRotateAngle);

                _currentAngle = _fireAngle + angleFromFire;
                transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            }
        }

        _dir = transform.right;
    }

    public void SetTarget(Transform target)
    {
        if (target == null) return;
        _target = target;
    }

    public void SetFollowTarget(Transform followTarget)
    {
        if (followTarget == null) return;
        _followTarget = followTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (damageable != null)
            {
                DamageInfo info = new DamageInfo { AttackPosition = transform.position, Damage = _damage };
                damageable.TakeDamage(info);
            }
        }

        Destroy(gameObject);
    }
}