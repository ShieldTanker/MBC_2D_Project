using UnityEngine;

[DisallowMultipleComponent]
public class WeaponController : MonoBehaviour
{
    public FHandWeapon F_HandWeapon;
    public BHandWeapon B_HandWeapon;
    public FShoulderWeapon F_ShoulderWeapon;
    public BShoulderWeapon B_ShoulderWeapon;

    private void Awake()
    {
        F_HandWeapon = GetComponentInChildren<FHandWeapon>();
        B_HandWeapon = GetComponentInChildren<BHandWeapon>();

        F_ShoulderWeapon = GetComponentInChildren<FShoulderWeapon>();
        B_ShoulderWeapon = GetComponentInChildren<BShoulderWeapon>();
    }
}
