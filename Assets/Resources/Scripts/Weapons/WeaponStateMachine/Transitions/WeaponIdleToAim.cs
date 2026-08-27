/// <summary>
/// 아이들(Idle) → 조준(Aim)
/// </summary>
public class WeaponIdleToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponIdleToAim(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted)
            return false;

        switch (_context.WeaponData?.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                return _context.WeaponFlag.AttackSequenceStarted;
            case WeaponFireMode.FullAuto:
                return _context.WeaponInput.AttackPressed;
        }
        return _context.WeaponFlag.AttackSequenceStarted;
    }

    public override void Clear() { }
}