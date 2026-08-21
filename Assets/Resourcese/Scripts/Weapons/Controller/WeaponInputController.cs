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
    public void FHandPerformedFire() => WeaponController.F_HandAnchor?.Weapon?.PerformedFire();
    public void BHandPerformedFire() => WeaponController?.B_HandAnchor?.Weapon?.PerformedFire();

    // 어깨무장 공격 시도
    public void FShoulderPerformedFire() => WeaponController?.F_ShoulderAnchor?.Weapon?.PerformedFire();
    public void BShoulderPerformedFire() => WeaponController?.B_ShoulderAnchor?.Weapon?.PerformedFire();
    #endregion

    #region Canceled
    // 손무장 공격 입력 해제
    public void FHandCanceledFire() => WeaponController?.F_HandAnchor?.Weapon?.CanceledFire();
    public void BHandCanceledFire() => WeaponController?.B_HandAnchor?.Weapon?.CanceledFire();

    // 어깨무장 공격 입력 해제
    public void FShoulderCanceledFire() => WeaponController?.F_ShoulderAnchor?.Weapon?.CanceledFire();
    public void BShoulderCanceledFire() => WeaponController?.B_ShoulderAnchor?.Weapon?.CanceledFire();
    #endregion
}
