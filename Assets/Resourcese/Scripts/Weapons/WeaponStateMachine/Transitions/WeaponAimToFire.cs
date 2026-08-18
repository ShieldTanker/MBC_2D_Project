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

        if (_context.WeaponData.FireMode == WeaponFireMode.SemiAuto)
        {
            // 새롭게 버튼을 누른 경우에만 발사
            return _context.WeaponFlag.AttackSequenceStarted;
        }

        if (_context.WeaponData.FireMode == WeaponFireMode.FullAuto)
        {
            return input.AttackPressed;
        }

        return false;
    }

    public override void Clear()
    {
    }
}