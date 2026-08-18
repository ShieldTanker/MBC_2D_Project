using System.Collections.Generic;
using UnityEngine;

// 아이들 상태
public class WeaponIdleState : State<WeaponStateType, WeaponContext>
{
    public WeaponIdleState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
    }

    public override void StateEnter()
    {
        _machine.Context.WeaponAnchorPos.rotation = Quaternion.Euler(new Vector3(0, 0, 300f));
        _machine.Context.WeaponFlag.AttackSequenceStarted = false;
        base.StateEnter();
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}