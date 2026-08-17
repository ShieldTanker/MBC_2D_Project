using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("손 무장")]
    public Weapon B_HandWeapon;
    public Weapon F_HandWeapon;

    [Header("어깨 무장")]
    public Weapon B_ShoulderWeapon;
    public Weapon F_ShoulderWeapon;

    #region Performed
    // 손무장 공격 시도
    public void FHandPerformedFire() => F_HandWeapon?.PerformedFire();
    public void BHandPerformedFire() => B_HandWeapon?.PerformedFire();

    // 어깨무장 공격 시도
    public void BShoulderPerformedFire() => B_ShoulderWeapon?.PerformedFire();
    public void FShoulderPerformedFire() => F_ShoulderWeapon?.PerformedFire();
    #endregion

    #region Canceled
    // 손무장 공격 입력 해제
    public void FHandCanceledFire() => F_HandWeapon?.CanceledFire();
    public void BHandCanceledFire() => B_HandWeapon?.CanceledFire();

    // 어깨무장 공격 입력 해제
    public void BShoulderCanceledFire() => B_ShoulderWeapon?.CanceledFire();
    public void FShoulderCanceledFire() => F_ShoulderWeapon?.CanceledFire();
    #endregion
}
