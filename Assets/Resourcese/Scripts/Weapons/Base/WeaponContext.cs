using UnityEngine;

public class WeaponContext : StateContext
{
    public WeaponData WeaponData;

    public LockOnController LockonController { get; set; }

    public Transform FirePosition { get; set; }
    public Transform WeaponPos { get; set; }

    public WeaponInput WeaponInput { get; set; }
    public WeaponFlag WeaponFlag { get; set; }

    public float FireRate { get; set; }
    public int CurrentCapacity { get; set; }
    public int AmmoRemaining { get; set; }

    public bool IsInterrupted { get; set; } = false;

    // 마지막 발사 이후 경과 시간
    public float LastFireTime { get; set; }
}
