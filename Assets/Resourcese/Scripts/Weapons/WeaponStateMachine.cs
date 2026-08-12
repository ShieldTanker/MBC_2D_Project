using System.Collections.Generic;

public enum WeaponStateType
{
    Idle, Fire, Reload, Holding,
}

public enum WeaponPosition
{
    F_Hand, B_Hand, F_Shoulder, B_Shoulder,
}

public class WeaponStateMachine : StateMachine<WeaponStateType, WeaponContext>
{
    protected override State<WeaponStateType, WeaponContext> CreateState(WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = this.Create(stateType);
        return state;
    }
}

public class WeaponContext : StateContext
{
    public bool IsReloading;
    public bool IsFire;
    public WeaponPosition WeaponPos;
}

public static partial class WeaponStateFactory
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
                transitions.Add(new WeaponStateToFire(machine.Context, WeaponStateType.Fire));
                transitions.Add(new WeaponStateToReload(machine.Context, WeaponStateType.Reload));

                // logics.Add();

                state = new WeaponIdleState(machine, transitions, logics);
                break;

            case WeaponStateType.Fire:
                transitions.Add(new WeaponStateToIdle(machine.Context, WeaponStateType.Idle));
                transitions.Add(new WeaponStateToReload(machine.Context, WeaponStateType.Reload));

                // logics.Add();
                state = new WeaponFireState(machine, transitions, logics);
                break;

            case WeaponStateType.Reload:
                transitions.Add(new WeaponStateToIdle(machine.Context, WeaponStateType.Idle));

                // logics.Add();
                state = new WeaponReloadState(machine, transitions, logics);
                break;

            case WeaponStateType.Holding:
                // transitions.Add();

                // logics.Add();
                state = new WeaponHoldingState(machine, transitions, logics);
                break;
        }

        return state;
    }
}
