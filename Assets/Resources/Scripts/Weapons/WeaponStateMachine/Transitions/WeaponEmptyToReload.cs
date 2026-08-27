
/// <summary>
/// 반동(Recoil) → 조준(Aim)
/// </summary>
public class WeaponEmptyToReload : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponEmptyToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return _context.AmmoRemaining > 0;
    }

    public override void Clear() { }
}
