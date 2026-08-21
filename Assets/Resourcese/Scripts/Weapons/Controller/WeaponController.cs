using UnityEngine;

[DisallowMultipleComponent]
public class WeaponController : MonoBehaviour
{
    [Header("락온 컨트롤러")]
    public LockOnController LockOnController;

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

        LockOnController = GetComponentInChildren<LockOnController>();
        IKController = GetComponentInChildren<IKController>();
    }

    private void Start()
    {
        F_HandAnchor.IKTarget = IKController.F_HandIK;
        B_HandAnchor.IKTarget = IKController.B_HandIK;

        SetAimTarget(LockOnController.GetTrackingTarget());
        SetLockonController();
    }

    public void SetAimTarget(Transform target)
    {
        F_HandAnchor.AimTarget = target;
        B_HandAnchor.AimTarget = target;
        F_ShoulderAnchor.AimTarget = target;
        B_ShoulderAnchor.AimTarget = target;
    }

    public void SetLockonController()
    {
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
    }
}
