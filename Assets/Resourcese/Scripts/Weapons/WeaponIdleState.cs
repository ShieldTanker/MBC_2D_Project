using System.Collections.Generic;

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