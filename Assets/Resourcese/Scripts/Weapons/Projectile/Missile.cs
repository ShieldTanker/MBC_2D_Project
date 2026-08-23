using UnityEngine;

public class Missile : BulletTest
{
    float _followStartTime = 1f;

    public override void Move()
    {
        if (_followTarget == null || _time < _followStartTime)
        {
            base.Move();
            return;
        }

        RotateToTarget();
        base.Move();
    }

    /// <summary> 발사각(_fireAngle) 기준 ±_maxRotateAngle 범위 내에서 타겟 방향으로 서서히 회전한다. </summary>
    protected void RotateToTarget()
    {
        if (_followTarget == null) return;

        Vector3 dir = _followTarget.position - transform.position;
        if (dir.sqrMagnitude < 0.0001f)
            return;

        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float angleFromFire = Mathf.DeltaAngle(_fireAngle, targetAngle);
        angleFromFire = Mathf.Clamp(angleFromFire, -_maxRotateAngle, _maxRotateAngle);
        float clampedTargetAngle = _fireAngle + angleFromFire;

        _currentAngle = Mathf.MoveTowardsAngle(_currentAngle, clampedTargetAngle, _rotateSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
        _dir = transform.right;
    }
}