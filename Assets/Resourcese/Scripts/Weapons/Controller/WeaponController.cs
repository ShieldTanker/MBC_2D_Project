using UnityEngine;

[DisallowMultipleComponent]
public class WeaponController : MonoBehaviour
{
    [Header("락온 컨트롤러")]
    public LockonController LockOnController;

    [Header("손 무장")]
    public FHandWeaponAimAnchor F_HandAnchor;
    public BHandWeaponAimAnchor B_HandAnchor;

    [Header("어깨 무장")]
    public FShoulderWeaponAimAnchor F_ShoulderAnchor;
    public BShoulderWeaponAimAnchor B_ShoulderAnchor;

    [Header("IK 컨트롤러")]
    public IKController IKController;

    private void Awake()
    {
        F_HandAnchor = GetComponentInChildren<FHandWeaponAimAnchor>();
        B_HandAnchor = GetComponentInChildren<BHandWeaponAimAnchor>();
        F_ShoulderAnchor = GetComponentInChildren<FShoulderWeaponAimAnchor>();
        B_ShoulderAnchor = GetComponentInChildren<BShoulderWeaponAimAnchor>();

        IKController = GetComponentInChildren<IKController>();
    }

    private void Start()
    {
        F_HandAnchor.IKTarget = IKController.F_HandIK;
        B_HandAnchor.IKTarget = IKController.B_HandIK;
    }

    public void SetLockonController(LockonController lockOnController)
    {
        LockOnController = lockOnController;

        // FrontHand
        F_HandAnchor.Weapon.Context.LockonController = LockOnController;
        F_HandAnchor.Weapon.LockonController = LockOnController;

        // BackHand
        B_HandAnchor.Weapon.Context.LockonController = LockOnController;
        B_HandAnchor.Weapon.LockonController = LockOnController;

        // FrontShoulder
        F_ShoulderAnchor.Weapon.Context.LockonController = LockOnController;
        F_ShoulderAnchor.Weapon.LockonController = LockOnController;

        // BackShoulder
        B_ShoulderAnchor.Weapon.Context.LockonController = LockOnController;
        B_ShoulderAnchor.Weapon.LockonController = LockOnController;

        F_HandAnchor.AimTarget = LockOnController.TrackingTargetTransform;
        B_HandAnchor.AimTarget = LockOnController.TrackingTargetTransform;
        F_ShoulderAnchor.AimTarget = LockOnController.TrackingTargetTransform;
        B_ShoulderAnchor.AimTarget = LockOnController.TrackingTargetTransform;

    }

    public void SetAlive(bool isAlive)
    {
        F_HandAnchor.CanRotate = isAlive;
        B_HandAnchor.CanRotate = isAlive;
        F_ShoulderAnchor.CanRotate = isAlive;
        B_ShoulderAnchor.CanRotate = isAlive;

        F_HandAnchor.Weapon.Context.WeaponFlag.IsAlive = isAlive;
        B_HandAnchor.Weapon.Context.WeaponFlag.IsAlive = isAlive;
        F_ShoulderAnchor.Weapon.Context.WeaponFlag.IsAlive = isAlive;
        B_ShoulderAnchor.Weapon.Context.WeaponFlag.IsAlive = isAlive;
    }

    #region 입력 관련 함수
    #region Performed
    // 손무장 공격 시도
    public void FHandPerformedFire() => F_HandAnchor?.Weapon?.PerformedFire();
    public void BHandPerformedFire() => B_HandAnchor?.Weapon?.PerformedFire();

    // 어깨무장 공격 시도
    public void FShoulderPerformedFire() => F_ShoulderAnchor?.Weapon?.PerformedFire();
    public void BShoulderPerformedFire() => B_ShoulderAnchor?.Weapon?.PerformedFire();
    #endregion

    #region Canceled
    // 손무장 공격 입력 해제
    public void FHandCanceledFire() => F_HandAnchor?.Weapon?.CanceledFire();
    public void BHandCanceledFire() => B_HandAnchor?.Weapon?.CanceledFire();

    // 어깨무장 공격 입력 해제
    public void FShoulderCanceledFire() => F_ShoulderAnchor?.Weapon?.CanceledFire();
    public void BShoulderCanceledFire() => B_ShoulderAnchor?.Weapon?.CanceledFire();
    #endregion
    #endregion
}
