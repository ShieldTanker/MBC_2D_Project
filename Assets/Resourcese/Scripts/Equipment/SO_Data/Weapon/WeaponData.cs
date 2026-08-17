using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("모델 관련")]
    public WeaponModel Model;
    public Vector3 Offset;

    [Header("스탯 관련")]
    public WeaponType WeaponType;
    public WeaponFireMode FireMode; // 단, 연발 사격모드

    public int Damage;              // 탄약 데미지
    public float FireRate;          // 연사력
    public float Range;             // 사거리

    public float MaxAimTime = 1f;   // 조준 완료까지 시간
    public float MaxReloadDuration; // 최대 재장전 시간

    [Header("탄약 정보")]
    public GameObject BulletModel;
    public float BulletSpeed;            // 탄속
    public int MaxCapacity;             // 최대 장탄수
    public int MaxAmmo;                 // 최대 탄약수

    [Header("반동 데이터")]
    public RecoilData RecoilData;
}

public class WeaponContext : StateContext
{
    // 무기 정보
    public WeaponData WeaponData;

    public Transform FirePosition { get; set; }         // 사격 위치
    public Transform WeaponPos { get; set; }            // 무기의 위치
    public Transform WeaponAnchorPos { get; set; }      // 무기 앵커 트랜스폼

    public Vector3 WeaponBaseLocalPos { get; set; }     // 무기의 시작 로컬위치
    public Quaternion WeaponBaseLocalRot { get; set; }  // 무기의 시작 로컬회전

    public WeaponInput WeaponInput { get; set; }
    public WeaponFlag WeaponFlag { get; set; }

    // 탄약
    public int CurrentCapacity { get; set; }            // 현재 장탄수
    public int AmmoRemaining { get; set; }              // 남은 탄약수

    public bool AttackSequenceStarted { get; set; }     // 공격 시작
    public bool IsInterrupted { get; set; } = false;    // 강제행동 여부

    public float AimIdleTimer { get; set; }
}

public class WeaponInput
{
    // 무장 입력
    public bool AttackPressed { get; set; } // 공격 입력
    public bool AttackHeld { get; set; }
    public bool AttackReleased { get; set; }

    public bool ReloadPressed { get; set; } // 재장전 눌림

    /// <summary>
    /// 플래그 전부 초기화
    /// </summary>
    public void ClearAll()
    {
        AttackPressed = false;
        AttackHeld = false;
        AttackReleased = false;

        ReloadPressed = false;
    }
}

public class WeaponFlag
{
    // 행동 가능 여부
    public bool CanFire { get; set; } = true;   // 사격 가능 여부
    public bool CanReload { get; set; }         // 재장전 가능

    // 완료 플래그
    public bool IsAimComplete { get; set; }     // 조준 완료 플래그
    public bool IsFireComplete { get; set; }    // 발사 완료 플래그
    public bool IsRecoilComplete { get; set; }  // 반동 완료 플래그
    public bool IsReloadComplete { get; set; }  // 재장전 완료 플래그 

    /// <summary>
    /// 플래그 전부 초기화
    /// </summary>
    public void ClearAll()
    {
        CanFire = false;
        CanReload = false;

        IsAimComplete = false;
        IsFireComplete = false;
        IsRecoilComplete = false;
        IsReloadComplete = false;
    }
}