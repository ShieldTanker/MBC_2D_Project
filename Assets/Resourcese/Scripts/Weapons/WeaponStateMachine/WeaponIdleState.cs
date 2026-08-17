using System.Collections.Generic;
using UnityEngine;

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

    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.WeaponAnchorPos.rotation = Quaternion.Euler(new Vector3(0, 0, 300f));
    }

    public override void StateExit()
    {
        base.StateExit();
        _machine.Context.WeaponInput.AttackPressed = false;
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

    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.WeaponFlag.IsAimComplete = false;
        _machine.Context.AimIdleTimer = 0f;
    }

    public override void StateExit()
    {
        base.StateExit();

        _machine.Context.WeaponFlag.IsAimComplete = false;
        _machine.Context.AimIdleTimer = 0f;
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

    public override void StateEnter()
    {
        base.StateEnter();

        WeaponContext context = _machine.Context;

        context.WeaponFlag.IsFireComplete = false;

        if (context.WeaponData == null)
        {
            context.WeaponFlag.IsFireComplete = true;
            return;
        }

        if (context.CurrentCapacity <= 0)
        {
            context.WeaponFlag.IsFireComplete = true;
            return;
        }

        if (context.WeaponData.BulletModel != null &&
            context.FirePosition != null)
        {
            GameObject.Instantiate(context.WeaponData.BulletModel, context.FirePosition.position, context.FirePosition.rotation);
        }

        context.CurrentCapacity--;
    }

    public override void StateExit()
    {
        base.StateExit();

        _machine.Context.WeaponFlag.IsFireComplete = false;
    }
}

// 반동 상태
public class WeaponRecoilState : State<WeaponStateType, WeaponContext>
{
    public WeaponRecoilState(WeaponStateMachine machine,
            List<StateTransition<WeaponStateType, WeaponContext>> transitions,
            List<StateLogic<WeaponContext>> logics)
    { _machine = machine; _transitions = transitions; _logics = logics; }

    public override void StateEnter()
    {
        base.StateEnter();

        _machine.Context.WeaponFlag.IsRecoilComplete = false;
        _machine.Context.WeaponPos.localPosition = _machine.Context.WeaponBaseLocalPos;
        _machine.Context.WeaponPos.localRotation = _machine.Context.WeaponBaseLocalRot;
    }

    public override void StateExit()
    {
        base.StateExit();

        _machine.Context.WeaponFlag.IsRecoilComplete = false;
        _machine.Context.WeaponPos.localPosition = _machine.Context.WeaponBaseLocalPos;
        _machine.Context.WeaponPos.localRotation = _machine.Context.WeaponBaseLocalRot;
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

    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.WeaponFlag.IsReloadComplete = false;
    }

    public override void StateExit()
    {
        base.StateExit();
        _machine.Context.WeaponFlag.IsReloadComplete = false;
    }
}

/* // 차징 상태
public class WeaponHoldingState : State<WeaponStateType, WeaponContext>
//{
//    public WeaponHoldingState(WeaponStateMachine machine,
//        List<StateTransition<WeaponStateType, WeaponContext>> transitions,
//        List<StateLogic<WeaponContext>> logics)
//    {
//        _machine = machine;
//        _transitions = transitions;
//        _logics = logics;
//    }
}
*/