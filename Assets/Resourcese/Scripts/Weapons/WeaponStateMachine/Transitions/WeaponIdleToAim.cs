/// <summary>
/// 아이들(Idle) → 조준(Aim)
/// </summary>
public class WeaponIdleToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponIdleToAim(WeaponContext context,WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsInterrupted)
            return false;

        return _context.WeaponFlag.AttackSequenceStarted && _context.WeaponInput.AttackPressed;
    }

    public override void Clear() { }
}