public class WeaponAimToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted 
            || _context.WeaponData == null) return true;

        // if (check) { _context.WeaponFlag.IsAimComplete = false; }
        return _context.WeaponFlag.IsAimCanceled;
    }

    public override void Clear() { }
}