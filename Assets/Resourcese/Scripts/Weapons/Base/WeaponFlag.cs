public class WeaponFlag
{
    public bool IsAlive { get; set; } = true;
    public bool CanFire { get; set; } = true;
    public bool CanReload { get; set; }
    public bool IsAiming { get; set; }

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