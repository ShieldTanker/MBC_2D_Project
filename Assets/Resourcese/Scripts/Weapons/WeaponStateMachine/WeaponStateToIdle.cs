public class WeaponStateToIdle : StateTransition<WeaponStateType, WeaponContext>
{
    float _time = 0f;
    public WeaponStateToIdle(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 발사 입력이 없고, 발사 상태가 아니며, 재장중이지 않을때
        if (!_context.IsFireInput && !_context.IsReloadInput)
        {
            _time += deltaTime;
            if (_time >= 2f) { return true; }
        }
        else
        { _time = 0f; }

        return false;
    }
}

public class WeaponStateToAim : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToAim(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 발사 입력이 있고, 발사 상태가 아닐때
        if (_context.IsFireInput && !_context.IsFireing)
        {
            return true;
        }

        return false;
    }
}

public class WeaponStateToFire : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToFire(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 발사 입력이 있고, 발사중 이 아니며, 재장전 입력이 없을때
        if (_context.IsFireInput  && !_context.IsFireing && !_context.IsReloadInput)
        { return true; }

        return false;
    }
}

public class WeaponStateToRecoil : StateTransition<WeaponStateType, WeaponContext>
{
    public WeaponStateToRecoil(WeaponContext context, WeaponStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 반동중이지 않을때
        if (!_context.IsRecoil)
        { return true; }

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
        if (_context.IsReloadInput) { return true; }

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
        if (_context.IsFireInput)
        {
            _time += deltaTime;
            if( _time > 2f) { return true; }
        }
        else { _time = 0f; }

        return false;
    }
}