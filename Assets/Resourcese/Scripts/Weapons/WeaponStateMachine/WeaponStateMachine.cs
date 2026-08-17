using System.Collections.Generic;
using UnityEngine;

public enum WeaponStateType
{
    Idle,
    Aim,
    Fire,
    Recoil,
    Reload,
    Holding,
}

public enum WeaponPosition
{
    F_Hand,
    B_Hand,
    F_Shoulder,
    B_Shoulder,
}

public enum WeaponType
{
    HandGun,
    Rifle,
    MachineGun,
    Melee,
    Launcher,
    Missile
}

/// <summary>
/// 단, 연발 사격모드
/// </summary>
public enum WeaponFireMode 
{
    SemiAuto,
    FullAuto,
}

public class WeaponStateMachine : StateMachine<WeaponStateType, WeaponContext>
{
    public WeaponStateMachine(WeaponContext context) { Context = context; }

    protected override State<WeaponStateType, WeaponContext> CreateState(WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = this.Create(stateType);
        return state;
    }
}

public static class WeaponStateFactory
{
    public static State<WeaponStateType, WeaponContext> Create(
        this WeaponStateMachine machine,
        WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = null;

        List<StateTransition<WeaponStateType, WeaponContext>> transitions = new();
        List<StateLogic<WeaponContext>> logics = new();

        switch (stateType)
        {
            case WeaponStateType.Idle:
                transitions.Add(new WeaponIdleToAim(machine.Context, WeaponStateType.Aim));
                transitions.Add(new WeaponIdleToReload(machine.Context, WeaponStateType.Reload));
                logics.Add(new WeaponIdleStateLogic(machine.Context));

                state = new WeaponIdleState(machine, transitions, logics);
                break;

            case WeaponStateType.Aim:
                transitions.Add(new WeaponAimToFire(machine.Context, WeaponStateType.Fire));
                transitions.Add(new WeaponAimToReload(machine.Context, WeaponStateType.Reload));
                transitions.Add(new WeaponAimToIdle(machine.Context, WeaponStateType.Idle));
                logics.Add(new WeaponAimStateLogic(machine.Context));

                state = new WeaponAimState(machine, transitions, logics);
                break;

            case WeaponStateType.Fire:
                transitions.Add(new WeaponFireToRecoil(machine.Context, WeaponStateType.Recoil));
                logics.Add(new WeaponFireStateLogic(machine.Context));

                state = new WeaponFireState(machine, transitions, logics);
                break;

            case WeaponStateType.Recoil:
                transitions.Add(new WeaponRecoilToAim(machine.Context, WeaponStateType.Aim));
                logics.Add(new WeaponRecoilStateLogic(machine.Context));

                state = new WeaponRecoilState(machine, transitions, logics);
                break;

            case WeaponStateType.Reload:
                transitions.Add(new WeaponReloadToIdle(machine.Context, WeaponStateType.Idle));
                logics.Add(new WeaponReloadStateLogic(machine.Context));

                state = new WeaponReloadState(machine, transitions, logics);
                break;
        }

        return state;
    }
}