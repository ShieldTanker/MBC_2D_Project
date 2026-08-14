using System.Collections.Generic;
using UnityEngine;

public enum WeaponStateType
{
    Idle, Aim, Fire, Recoil, Reload, Holding,
}

public enum WeaponPosition
{
    F_Hand, B_Hand, F_Shoulder, B_Shoulder,
}

public class WeaponContext : StateContext
{
    public bool IsFireInput = false;
    public bool IsAiming = false;
    public bool IsFireing = false;
    public bool IsRecoil = false;

    public int CurrentCapacity;     // 현재 장탄수
    public int MaxCapacity;         // 최대 장탄수
    public int AmmoRemaining;       // 남은 탄약수
    public int MaxRemaining;        // 최대 탄약수

    public bool IsReloadInput = false;
    public bool IsReloading = false;

    public Transform FirePosition;

    public WeaponPosition WeaponPos;
}

public class WeaponStateMachine : StateMachine<WeaponStateType, WeaponContext>
{
    protected override State<WeaponStateType, WeaponContext> CreateState(WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = this.Create(stateType);
        return state;
    }
}

public static class WeaponStateFactory
{
    public static State<WeaponStateType, WeaponContext> Create(this WeaponStateMachine machine, WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = null;
        List<StateTransition<WeaponStateType, WeaponContext>> transitions = new();
        List<StateLogic<WeaponContext>> logics = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        // AgentStateMachine 에서 Context를 받음으로 AgentContext형식
        switch (stateType)
        {
            case WeaponStateType.Idle:
                // 사격은 에임 상태에서만 가능하게
                transitions.Add(new WeaponStateToAim(machine.Context, WeaponStateType.Aim));
                transitions.Add(new WeaponStateToReload(machine.Context, WeaponStateType.Reload));

                // logics.Add();

                state = new WeaponIdleState(machine, transitions, logics);
                break;
            case WeaponStateType.Aim:
                transitions.Add(new WeaponStateToFire(machine.Context, WeaponStateType.Fire));
                break;

            case WeaponStateType.Fire:
                //사격후 다시 에임으로
                transitions.Add(new WeaponStateToRecoil(machine.Context, WeaponStateType.Recoil));

                // logics.Add();
                state = new WeaponFireState(machine, transitions, logics);
                break;

            case WeaponStateType.Recoil:
                // 반동제어 후 조준상태 혹은 재장전
                transitions.Add(new WeaponStateToAim(machine.Context, WeaponStateType.Aim));
                break;

            case WeaponStateType.Reload:
                transitions.Add(new WeaponStateToIdle(machine.Context, WeaponStateType.Idle));

                // logics.Add();
                state = new WeaponReloadState(machine, transitions, logics);
                break;

            /*
            case WeaponStateType.Holding:
                transitions.Add(new WeaponStateToIdle(machine.Context, WeaponStateType.Idle));

                // logics.Add();
                state = new WeaponHoldingState(machine, transitions, logics);
                break;*/

        }

        return state;
    }
}
