using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("모델 관련")]
    public WeaponModel Model;
    public Vector3 Offset;

    [Header("스탯 관련")]
    public WeaponType WeaponType;
    public WeaponFireMode FireMode;

    public int Damage;
    public float FireRate;
    public float Range;

    public float MaxAimTime = 1f;
    public float MaxReloadDuration;

    [Header("탄약 정보")]
    public GameObject BulletModel;
    public float BulletSpeed;
    public int MaxCapacity;
    public int MaxAmmo;

    [Header("반동 데이터")]
    public RecoilData RecoilData;
}

public class WeaponContext : StateContext
{
    public WeaponData WeaponData;

    public Transform FirePosition { get; set; }
    public Transform WeaponPos { get; set; }
    public Transform WeaponAnchorPos { get; set; }

    public Vector3 WeaponBaseLocalPos { get; set; }
    public Quaternion WeaponBaseLocalRot { get; set; }

    public Vector3 recoilVelocity = Vector3.zero;

    public WeaponInput WeaponInput { get; set; }
    public WeaponFlag WeaponFlag { get; set; }

    public int CurrentCapacity { get; set; }
    public int AmmoRemaining { get; set; }

    public bool IsInterrupted { get; set; } = false;

    // 마지막 발사 이후 경과 시간
    public float TimeSinceLastFire { get; set; }
}

public class WeaponInput
{
    /// <summary>
    /// 현재 공격 버튼을 누르고 있는지 여부.
    /// 버튼을 누르고 있는 동안 true.
    /// </summary>
    public bool AttackPressed { get; set; }

    /// <summary>
    /// 공격 버튼을 누르고 있는 동안의 Hold 상태.
    /// </summary>
    public bool AttackHold { get; set; }

    /// <summary>
    /// 재장전 입력.
    /// </summary>
    public bool InteractionPressed { get; set; }

    public void ClearAll()
    {
        AttackPressed = false;
        AttackHold = false;
        InteractionPressed = false;
    }
}

public class WeaponFlag
{
    public bool CanFire { get; set; } = true;
    public bool CanReload { get; set; }

    public bool IsAimComplete { get; set; }
    public bool IsAimCanceled { get; set; }

    /// <summary>
    /// 공격 버튼을 새롭게 눌렀을 때 발생하는 1회성 이벤트.
    /// FireState에서 소비합니다.
    /// </summary>
    public bool AttackSequenceStarted { get; set; }
    public bool IsRecoilComplete { get; set; }
    public bool IsReloadComplete { get; set; }

    public void ClearAll()
    {
        CanFire = false;
        CanReload = false;
        AttackSequenceStarted = false;
        IsAimComplete = false;
        IsAimCanceled = false;
        IsRecoilComplete = false;
        IsReloadComplete = false;
    }
}