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

        if (_context.WeaponData.FireMode == WeaponFireMode.SemiAuto)
            return !_context.WeaponFlag.AttackSequenceStarted;
                
        return !input.AttackPressed;
    }

    public override void Clear() { }
}