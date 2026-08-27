using System.Collections.Generic;

public class WeaponEmptyState : State<WeaponStateType, WeaponContext>
{
    WeaponFlag _flag;
    public WeaponEmptyState(WeaponStateMachine machine,
            List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
        _flag = _machine.Context.WeaponFlag;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _flag.IsAiming = false;
        _flag.IsAimComplete = false;
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}