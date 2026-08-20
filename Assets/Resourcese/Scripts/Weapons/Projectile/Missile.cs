using UnityEngine;

public class Missile : BulletTest
{
    float _followStartTime = 1f;

    public override void Move()
    {
        if (_followTarget == null)
        {
            base.Move();
            return;
        }
        
        if (_time < _followStartTime)
        {
            base.Move();
            return;
        }

        dir = _followTarget.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.eulerAngles.z, angle, _rotateSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, angle);
        base.Move();
    }
}