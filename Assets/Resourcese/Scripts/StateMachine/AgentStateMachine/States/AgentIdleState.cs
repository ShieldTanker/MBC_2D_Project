using System.Collections.Generic;

public class AgentIdleState : State<AgentStateType, AgentContext>
{
    public AgentIdleState(
        StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}