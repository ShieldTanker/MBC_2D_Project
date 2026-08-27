public class WeaponAimToFire : StateTransition<WeaponStateType, WeaponContext>
{
    WeaponInput _input;
    WeaponFlag _flag;
    public WeaponAimToFire(WeaponContext context, WeaponStateType stateType) : base(context, stateType)
    {
        _input = _context.WeaponInput;
        _flag = _context.WeaponFlag;
    }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted) return false;

        if (!_flag.IsAimComplete) return false;

        if (!_flag.CanFire) return false;

        if (_input.InteractionPressed && _input.AttackPressed
            && _context.CurrentCapacity >= 0 && _context.CurrentCapacity < _context.WeaponData.MaxCapacity)
            return false;

        switch (_context.WeaponData.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                return _flag.AttackSequenceStarted;

            case WeaponFireMode.FullAuto:
                return _flag.AttackSequenceStarted || _input.AttackPressed;

            case WeaponFireMode.Burst:
                return _flag.AttackSequenceStarted;
        }

        return false;
    }

    public override void Clear() { }
}