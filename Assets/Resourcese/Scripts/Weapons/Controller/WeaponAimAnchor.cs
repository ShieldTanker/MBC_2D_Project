using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    private Weapon _weapon;
    public Transform EffectTarget;

    public bool UseAim = true;
    public Vector3 BaseRot = new Vector3(0, 0, 0);
    public Vector3 currRot;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(BaseRot);
        _weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        currRot = transform.rotation.eulerAngles;
        if (!UseAim || _weapon == null) return;

        EffectTarget.position = _weapon.transform.position;
        EffectTarget.rotation = _weapon.transform.rotation;
    }

    public Weapon GetWeapon()
    {
        return _weapon;
    }

    public void SetTarget(Transform target)
    {
        if (_weapon == null) return;
        _weapon.AimTarget = target;
    }
}
