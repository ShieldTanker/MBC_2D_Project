using UnityEngine.Rendering;

/// <summary>
/// 아이들(Idle) → 조준(Aim)
/// </summary>
public class WeaponIdleToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponIdleToAim(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // if (_context.IsInterrupted) return false;
        if (!_context.WeaponInput.AttackPressed) return false;

        _context.AttackSequenceStarted = true;

        return true;
    }

    public override void Clear() { }
}

/// <summary>
/// 아이들(Idle) → 재장전(Reload)
/// </summary>
public class WeaponIdleToReload : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponIdleToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return _context.WeaponInput.ReloadPressed && CanReload();
    }

    private bool CanReload()
    {
        if (_context.WeaponData == null) return false;
        if (_context.CurrentCapacity >= _context.WeaponData.MaxCapacity) return false;

        return _context.AmmoRemaining > 0;
    }

    public override void Clear() { }
}

/// <summary>
/// 조준(Aim) → 발사(Fire)
/// </summary>
public class WeaponAimToFire : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToFire(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted) return false;
        if (!_context.WeaponFlag.IsAimComplete) return false;
        if (!_context.WeaponFlag.CanFire) return false;

        return CheckAttack();
    }

    private bool CheckAttack()
    {
        // Aim 진입으로 시작된 첫 발
        if (_context.AttackSequenceStarted)
        {
            _context.AttackSequenceStarted = false;
            return true;
        }

        // 연사
        if (_context.WeaponData.FireMode == WeaponFireMode.FullAuto && _context.WeaponInput.AttackHeld)
        {
            return true;
        }

        // 단발 재입력
        if (_context.WeaponData.FireMode == WeaponFireMode.SemiAuto && _context.WeaponInput.AttackPressed)
        {
            return true;
        }

        return false;
    }

    public override void Clear() { }
}

/// <summary>
/// 조준(Aim) → 재장전(Reload)
/// </summary>
public class WeaponAimToReload : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return CanReload() && ShouldReload();
    }

    private bool CanReload()
    {
        if (_context.WeaponData == null) return false;
        if (_context.CurrentCapacity >= _context.WeaponData.MaxCapacity) return false;
        if (_context.AmmoRemaining <= 0) return false;

        return true;
    }

    private bool ShouldReload()
    {
        bool reloadPressed = _context.WeaponInput.ReloadPressed;
        bool magazineEmpty = _context.CurrentCapacity <= 0;

        return reloadPressed || magazineEmpty;
    }

    public override void Clear() { }
}

/// <summary>
/// 조준(Aim) → 아이들(Idle)
/// </summary>
public class WeaponAimToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted) return true;

        return _context.AimIdleTimer >= _context.WeaponData.MaxAimTime;
    }

    public override void Clear() { }
}

/// <summary>
/// 발사(Fire) → 반동(Recoil)
/// </summary>
public class WeaponFireToRecoil : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponFireToRecoil(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 발사가 끝났을때
        return _context.WeaponFlag.IsFireComplete;
    }

    public override void Clear() { }
}

/// <summary>
/// 반동(Recoil) → 조준(Aim)
/// </summary>
public class WeaponRecoilToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponRecoilToAim(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 강제 이동중(닷지 등) 혹은 반동이 끝났을때 true
        return _context.WeaponFlag.IsRecoilComplete || _context.IsInterrupted;
    }

    public override void Clear() { }
}

/// <summary>
/// 재장전(Reload) → 아이들(Idle)
/// </summary>
public class WeaponReloadToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponReloadToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 장전이 끝났을때
        return _context.WeaponFlag.IsReloadComplete;
    }

    public override void Clear() { }
}