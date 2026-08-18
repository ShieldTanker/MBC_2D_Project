using System.Collections.Generic;

public class WeaponEmptyState : State<WeaponStateType, WeaponContext>
{
    public WeaponEmptyState(WeaponStateMachine machine,
            List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
    }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}