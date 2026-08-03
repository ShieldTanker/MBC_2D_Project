using System.Collections.Generic;
using UnityEngine;

public class AgentMoveState : State<AgentStateType, AgentContext>
{
    public AgentMoveState
        (StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}
