using UnityEngine;

[DisallowMultipleComponent]
public class BShoulderWeaponAimAnchor : WeaponAimAnchor
{
    [SerializeField] GameObject subArm;

    protected override void BaseOnEnable()
    {
        base.BaseOnDisable();
        Weapon.OnWeaponChanged += SubArmCheck;
    }

    protected override void BaseOnDisable()
    {
        base.BaseOnDisable();
        Weapon.OnWeaponChanged -= SubArmCheck;
    }

    void SubArmCheck()
    {
        if (subArm == null) return;
        subArm.SetActive(Weapon.WeaponData != null);
    }
}