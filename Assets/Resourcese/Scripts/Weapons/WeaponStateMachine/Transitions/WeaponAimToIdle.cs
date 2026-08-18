public class WeaponAimToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted 
            || _context.WeaponData == null) return true;

        return _context.TimeSinceLastFire >= 2f;
    }

    public override void Clear() { }
}