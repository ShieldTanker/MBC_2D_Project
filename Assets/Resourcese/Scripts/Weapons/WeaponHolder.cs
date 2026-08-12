using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private Transform _firePos;

    public Transform Target { get; set; }
    public Transform HolderPosition;
}

public class Weapon : MonoBehaviour
{
    WeaponStateMachine _machine;
    private RecoilData _recoilData;

    private WeaponData _weaponData;
    public WeaponData WeaponData
    {
        get { return _weaponData; }
        set
        {
            _weaponData = value;
            SetWeapon();
        }
    }
    public WeaponData _baseData;

    public WeaponHolder Holder;

    public WeaponPosition WeaponPos;

    public void ChangeWeaponData(WeaponData data)
    {

    }

    void SetWeapon()
    {
        _recoilData = _weaponData.RecoilData;
    }
}