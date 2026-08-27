using System;
using System.Collections.Generic;

public static class AgentStateFactory
{
    /// <summary>
    /// 호출하는 상태머신을 매개변수로 원하는 타입의 상태 초기화
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public static State<AgentStateType, AgentContext> Create(this AgentStateMachine machine, AgentStateType stateType)
    {
        State<AgentStateType, AgentContext> state = null;
        List<StateTransition<AgentStateType, AgentContext>> transitions = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        // AgentStateMachine 에서 Context를 받음으로 AgentContext형식
        switch (stateType)
        {
            case AgentStateType.Idle:
                transitions.Add(new AgentStateToDie(machine.Context, AgentStateType.Died));
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));
                transitions.Add(new AgentStateToJump(machine.Context, AgentStateType.Jump));
                transitions.Add(new AgentStateToOnAir(machine.Context, AgentStateType.OnAir));

                state = new AgentIdleState(machine, transitions);
                break;

            case AgentStateType.Move:
                transitions.Add(new AgentStateToDie(machine.Context, AgentStateType.Died));
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToJump(machine.Context, AgentStateType.Jump));
                transitions.Add(new AgentStateToOnAir(machine.Context, AgentStateType.OnAir));

                state = new AgentMoveState(machine, transitions);
                break;

            case AgentStateType.Jump:
                transitions.Add(new AgentStateToDie(machine.Context, AgentStateType.Died));
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));
                transitions.Add(new AgentStateToOnAir(machine.Context, AgentStateType.OnAir));

                // 점프중에는 이동은 막지만 회피는 가능
                state = new AgentJumpState(machine, transitions);
                break;

            case AgentStateType.OnAir:
                transitions.Add(new AgentStateToDie(machine.Context, AgentStateType.Died));
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));

                state = new AgentOnAirState(machine, transitions);
                break;

            case AgentStateType.Died:
                state = new AgentDieState(machine, transitions);
                break;
        }

        return state;
    }
}
