
/// <summary>
/// 재장전(Reload) → 아이들(Idle)
/// </summary>
public class WeaponReloadToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponReloadToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 장전이 끝났을때
        return _context.WeaponFlag.IsReloadComplete;
    }

    public override void Clear() { }
}