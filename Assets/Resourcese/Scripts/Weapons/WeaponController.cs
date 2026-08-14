using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("손 무장")]
    public Weapon B_HandWeapon;
    public Weapon F_HandWeapon;

    [Header("어깨 무장")]
    public Weapon B_ShoulderWeapon;
    public Weapon F_ShoulderWeapon;


    // 손무장 공격시도
    public void FHandTryFire() => F_HandWeapon?.TryFire();
    public void BHandTryFire() => B_HandWeapon?.TryFire();

    // 어깨무장 공격시도
    public void BShoulderTryFire() => B_ShoulderWeapon?.TryFire();
    public void FShoulderTryFire() => F_ShoulderWeapon?.TryFire();
}
