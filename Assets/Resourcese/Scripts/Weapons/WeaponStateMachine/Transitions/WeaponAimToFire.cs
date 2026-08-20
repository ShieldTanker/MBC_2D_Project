public class WeaponAimToFire : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponAimToFire(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted) return false;

        if (!_context.WeaponFlag.IsAimComplete) return false;

        if (!_context.WeaponFlag.CanFire) return false;

        WeaponInput input = _context.WeaponInput;

        switch (_context.WeaponData.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                return _context.WeaponFlag.AttackSequenceStarted;

            case WeaponFireMode.FullAuto:
                return _context.WeaponFlag.AttackSequenceStarted || input.AttackPressed;

            case WeaponFireMode.Burst:
                return _context.WeaponFlag.AttackSequenceStarted;
        }

        return false;
    }

    public override void Clear()
    {
    }
}