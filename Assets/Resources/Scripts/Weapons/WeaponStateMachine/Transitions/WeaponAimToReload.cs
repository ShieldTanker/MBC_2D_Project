/// <summary> 조준(Aim) → 재장전(Reload) </summary>
public class WeaponAimToReload : StateTransition<WeaponStateType, WeaponContext>
{
    WeaponInput _input;
    public WeaponAimToReload(WeaponContext context, WeaponStateType stateType) : base(context, stateType)
    {
        _input = _context.WeaponInput;
    }

    public override bool CheckStateTransit(float deltaTime)
    {
        return CanReload() && ShouldReload();
    }

    /// <summary> 재장전 할 수 있는지 검사 </summary>
    /// <returns></returns>
    private bool CanReload()
    {
        if (_context.WeaponData == null) return false;
        if (_context.CurrentCapacity >= _context.WeaponData.MaxCapacity || _context.AmmoRemaining <= 0)
            return false;
        // 장탄수가 0 ~ MaxCapacity 미만 이면 true, 남은 탄약수 0 초과면 true
        return true;
    }

    /// <summary> 재장전 해야 하는지 여부검사 </summary>
    /// <returns></returns>
    private bool ShouldReload()
    {
        bool reloadPressed = _input.InteractionPressed && _input.AttackPressed;

        if (_input.InteractionPressed)
        {
            UnityEngine.Debug.Log("재장전 입력 확인됨");
        }
        if (_input.AttackPressed)
        {
            UnityEngine.Debug.Log("공격 입력 확인됨");
        }
        if (reloadPressed)
        {
            UnityEngine.Debug.Log("재장전 시퀀스 확인됨");
        }
        
        bool magazineEmpty = _context.CurrentCapacity <= 0;
        // 남은 탄약수가 0이하거나 장전입력이 있으면 true;
        return magazineEmpty || reloadPressed;
    }

    public override void Clear() { }
}
