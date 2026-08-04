using System.Collections.Generic;

public class AgentJumpState : State<AgentStateType,AgentContext>
{
    public AgentJumpState
        (StateMachine<AgentStateType, AgentContext> stateMachine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = stateMachine;
        _transitions = transitions;
        _logics = logics;
    }
}