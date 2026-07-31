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
        List<StateLogic<AgentContext>> logics = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        // AgentStateMachine 에서 Context를 받음으로 AgentContext형식
        switch (stateType)
        {
            case AgentStateType.Idle:
                transitions.Add(new AgentIdleToMove(machine.Context));
                logics.Add(new AgentIdleStateLogic(machine.Context));
                state = new AgentIdleState(machine, transitions, logics);
                break;

            case AgentStateType.Move:
                break;
        }

        return state;
    }
}
