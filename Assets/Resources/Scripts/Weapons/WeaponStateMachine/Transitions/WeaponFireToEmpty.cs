
/// <summary>
/// 발사(Fire) → 탄약없음(Empty)
/// </summary>
public class WeaponFireToEmpty : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponFireToEmpty(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 현재 장탄수가 0이하이면 Empty로 이동
        return _context.CurrentCapacity <= 0;
    }

    public override void Clear() { }
}
