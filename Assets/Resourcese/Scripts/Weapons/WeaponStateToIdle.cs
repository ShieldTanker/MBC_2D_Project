public static partial class WeaponStateFactory
{
    public class WeaponStateToIdle : StateTransition<WeaponStateType, WeaponContext>
    {
        float _time = 0f;
        public WeaponStateToIdle(WeaponContext context, WeaponStateType stateType)
            : base(context, stateType) { }

        public override bool CheckStateTransit(float deltaTime)
        {
            if (!_context.IsFire && !_context.IsReloading)
            {
                _time += deltaTime;
                if (_time >= 2f) { return true; }
            }
            else 
                { _time = 0f; }

            return false;
        }
    }
}

public class WeaponStateToFire : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToFire(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsFire) { return true; }

        return false;
    }
}

public class WeaponStateToReload : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToReload(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 나중에 WasPressedFrame으로 바꾸기
        if (_context.IsReloading) { return true; }

        return false;
    }
}

public class WeaponStateToHolding : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToHolding(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    float _time = 0f;

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.IsFire)
        {
            _time += deltaTime;
            if( _time > 2f) { return true; }
        }
        else { _time = 0f; }

        return false;
    }
}