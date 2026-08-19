using UnityEngine;

public class WeaponInputController : MonoBehaviour
{
    public WeaponAimController WeaponAimController;
    [Header("락온 컨트롤러")]
    public LockOnController LockOnController;
    private void Start()
    {
        LockOnController = GetComponentInChildren<LockOnController>();
        WeaponAimController = GetComponentInChildren<WeaponAimController>();

        Transform target = LockOnController.GetTrackingTarget();
        WeaponAimController.SetAimTarget(target);
    }

    #region Performed
    // 손무장 공격 시도
    public void FHandPerformedFire() => WeaponAimController?.F_HandAnchor?.GetWeapon()?.PerformedFire();
    public void BHandPerformedFire() => WeaponAimController?.B_HandAnchor?.GetWeapon()?.PerformedFire();

    // 어깨무장 공격 시도
    public void BShoulderPerformedFire() => WeaponAimController?.B_ShoulderAnchor?.GetWeapon()?.PerformedFire();
    public void FShoulderPerformedFire() => WeaponAimController?.F_ShoulderAnchor?.GetWeapon()?.PerformedFire();
    #endregion

    #region Canceled
    // 손무장 공격 입력 해제
    public void FHandCanceledFire() => WeaponAimController?.F_HandAnchor?.GetWeapon()?.CanceledFire();
    public void BHandCanceledFire() => WeaponAimController?.B_HandAnchor?.GetWeapon()?.CanceledFire();

    // 어깨무장 공격 입력 해제
    public void BShoulderCanceledFire() => WeaponAimController?.B_ShoulderAnchor?.GetWeapon()?.CanceledFire();
    public void FShoulderCanceledFire() => WeaponAimController?.F_ShoulderAnchor?.GetWeapon()?.CanceledFire();
    #endregion
}
