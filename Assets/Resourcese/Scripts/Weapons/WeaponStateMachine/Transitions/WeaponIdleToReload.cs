/// <summary>
/// 아이들(Idle) → 재장전(Reload)
/// </summary>
public class WeaponIdleToReload :
    StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponIdleToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return _context.WeaponInput.InteractionPressed &&
               CanReload();
    }

    private bool CanReload()
    {
        if (_context.WeaponData == null)
            return false;

        if (_context.CurrentCapacity >= _context.WeaponData.MaxCapacity)
        {
            return false;
        }

        return _context.AmmoRemaining > 0;
    }

    public override void Clear()
    {
    }
}