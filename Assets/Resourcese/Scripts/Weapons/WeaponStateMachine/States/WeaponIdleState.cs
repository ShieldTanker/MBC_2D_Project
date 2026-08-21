using System.Collections.Generic;
using UnityEngine;

// 아이들 상태
public class WeaponIdleState : State<WeaponStateType, WeaponContext>
{
    WeaponContext _context;
    public WeaponIdleState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        // 사격 관련
        _machine.Context.LastFireTime = 0f;
        _machine.Context.WeaponFlag.AttackSequenceStarted = false;

        // 조준 관련
        _machine.Context.WeaponFlag.IsAiming = false;
        _machine.Context.WeaponFlag.IsAimComplete = false;

        _machine.Context.Weapon.OnIdleStart?.Invoke();
        base.StateEnter();
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}