using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(WeaponController))]
public class WeaponInputController : MonoBehaviour
{
    public WeaponController WeaponController;

    private void Start()
    {
        WeaponController = GetComponentInChildren<WeaponController>();
    }

    #region Performed
    // 손무장 공격 시도
    public void FHandPerformedFire() => WeaponController.F_HandWeapon?.PerformedFire();
    public void BHandPerformedFire() => WeaponController?.B_HandWeapon?.PerformedFire();

    // 어깨무장 공격 시도
    public void BShoulderPerformedFire() => WeaponController?.B_ShoulderWeapon?.PerformedFire();
    public void FShoulderPerformedFire() => WeaponController?.F_ShoulderWeapon?.PerformedFire();
    #endregion

    #region Canceled
    // 손무장 공격 입력 해제
    public void FHandCanceledFire() => WeaponController?.F_HandWeapon?.CanceledFire();
    public void BHandCanceledFire() => WeaponController?.B_HandWeapon?.CanceledFire();

    // 어깨무장 공격 입력 해제
    public void FShoulderCanceledFire() => WeaponController?.F_ShoulderWeapon?.CanceledFire();
    public void BShoulderCanceledFire() => WeaponController?.B_ShoulderWeapon?.CanceledFire();
    #endregion
}
