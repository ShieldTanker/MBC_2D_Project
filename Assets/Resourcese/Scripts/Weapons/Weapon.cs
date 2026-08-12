using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform _target;
    private Transform _firePos;
    public WeaponData _weaponData;
    public WeaponData _baseData;

    public WeaponData WeaponData
    {
        get { return _weaponData; } 
        set { _weaponData = value; }
    }
}
