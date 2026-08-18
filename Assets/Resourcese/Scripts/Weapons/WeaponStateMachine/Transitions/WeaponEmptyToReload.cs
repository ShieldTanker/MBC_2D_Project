
/// <summary>
/// 반동(Recoil) → 조준(Aim)
/// </summary>
public class WeaponEmptyToReload : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponEmptyToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 강제 이동중(닷지 등) 혹은 반동이 끝났을때 true
        return _context.AmmoRemaining > 0;
    }

    public override void Clear() { }
}
