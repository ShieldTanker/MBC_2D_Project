/// <summary> 아이들(Idle) → 재장전(Reload) </summary>
public class WeaponIdleToReload : StateTransition<WeaponStateType, WeaponContext>
{
    WeaponInput _input;
    public WeaponIdleToReload(WeaponContext context, WeaponStateType stateType) : base(context, stateType)
    {
        _input = _context.WeaponInput;
    }

    public override bool CheckStateTransit(float deltaTime)
    {
        return (_input.InteractionPressed && _input.AttackPressed) && CanReload();
    }

    private bool CanReload()
    {
        if (_context.WeaponData == null)
            return false;

        if (_context.CurrentCapacity >= _context.WeaponData.MaxCapacity)
        {
            return false;
        }

        return _context.AmmoRemaining > 0;
    }

    public override void Clear()
    {
    }
}