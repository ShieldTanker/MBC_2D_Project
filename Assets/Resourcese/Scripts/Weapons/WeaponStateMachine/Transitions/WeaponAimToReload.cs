
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
        bool reloadPressed = _context.WeaponInput.InteractionPressed;
        bool magazineEmpty = _context.CurrentCapacity <= 0;

        return reloadPressed || magazineEmpty;
    }

    public override void Clear() { }
}
