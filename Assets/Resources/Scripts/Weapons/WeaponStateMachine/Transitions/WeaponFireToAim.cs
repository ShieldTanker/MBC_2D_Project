/// <summary>
/// 발사(Fire) → 조준(Aim)
/// </summary>
public class WeaponFireToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponFireToAim(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        WeaponInput input = _context.WeaponInput;
        switch (_context.WeaponData.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                return !_context.WeaponFlag.AttackSequenceStarted;
            case WeaponFireMode.FullAuto:
                return !input.AttackPressed;
            case WeaponFireMode.Burst:
                return !(_context.BurstRemaining > 0);
        }
        return false;
    }

    public override void Clear() { }
}