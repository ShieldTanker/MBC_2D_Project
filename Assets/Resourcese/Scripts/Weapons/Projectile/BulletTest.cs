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

    protected Transform _fireTransform;
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
        _damage = data.Damage;
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
        _fireTransform = fireTransform;

        // fireTransform.rotation은 부모(캐릭터)의 음수 스케일(좌우 반전)을 반영하지 못해
        // 반전 시 실제 화면상 방향과 어긋난다. TransformPoint는 스케일까지 포함한
        // 전체 행렬을 사용하므로 미러링된 실제 방향을 구할 수 있다.
        Vector3 worldRight = fireTransform.TransformPoint(Vector3.right) - fireTransform.position;
        _fireAngle = Mathf.Atan2(worldRight.y, worldRight.x) * Mathf.Rad2Deg;

        transform.position = fireTransform.position;
        transform.rotation = Quaternion.Euler(0, 0, _fireAngle);

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
        // if (collision.transform.root == _fireTransform.root) return;

        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (damageable != null)
            {
                Debug.Log("asdasdasdasdad");
                DamageInfo info = new DamageInfo { AttackPosition = transform.position, Damage = _damage };
                damageable.TakeDamage(info);
            }
        }

        Destroy(gameObject);
    }
}