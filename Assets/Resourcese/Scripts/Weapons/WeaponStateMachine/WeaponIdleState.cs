using System.Collections.Generic;

// 아이들 상태
public class WeaponIdleState : State<WeaponStateType, WeaponContext>
{
    public WeaponIdleState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

// 에임 상태
public class WeaponAimState : State<WeaponStateType, WeaponContext>
{
    public WeaponAimState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

// 사격 상태
public class WeaponFireState : State<WeaponStateType, WeaponContext>
{
    public WeaponFireState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

// 반동 상태
public class WeaponRecoilState : State<WeaponStateType, WeaponContext>
{
    public WeaponRecoilState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

// 재장전 상태
public class WeaponReloadState : State<WeaponStateType, WeaponContext>
{
    public WeaponReloadState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

// 차징 상태
public class WeaponHoldingState : State<WeaponStateType, WeaponContext>
{
    public WeaponHoldingState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
        List<StateLogic<WeaponContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}