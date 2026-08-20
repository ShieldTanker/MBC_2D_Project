using UnityEngine;

public abstract class WeaponAimAnchor : MonoBehaviour
{
    public Transform IKTarget { get; set; }
    public Transform AimTarget { get; set; }
    public Transform currentLockonTarget;

    [Space]
    public float AimSpeed = 10f;
    public bool UseAim = true;
    float angle = 0f;

    public Vector3 IdleRoate = new Vector3(0, 0, 0);
    public Vector3 currRot;

    public WeaponBase Weapon { get; private set; }
    private WeaponContext WeaponContext;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(IdleRoate);
        Weapon = GetComponentInChildren<WeaponBase>();
        WeaponContext = Weapon.Context;
    }

    void Update()
    {
        currRot = transform.rotation.eulerAngles;
        if (!UseAim || Weapon == null || IKTarget == null)
            return;

        IKTarget.position = Weapon.transform.position;
        IKTarget.rotation = Weapon.transform.rotation;

        // currentLockonTarget = WeaponContext.LockonController.GetCurrentTarget();

        Aiming();
    }

    private void Aiming()
    {
        if (WeaponContext.WeaponFlag.IsAiming)
        {
            Vector3 dir = AimTarget.position - transform.position;
            WeaponAim(dir);
        }
    }

    private void WeaponAim(Vector3 dir)
    {
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.eulerAngles.z, angle, AimSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetRotateIdle()
    {
        transform.rotation = Quaternion.Euler(IdleRoate);
    }
}
