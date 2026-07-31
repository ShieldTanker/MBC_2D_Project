public class AgentStateMachine : StateMachine<AgentStateType, AgentContext>
{
    public AgentStateMachine(AgentContext context)
    { Context = context; }

    protected override State<AgentStateType, AgentContext> CreateState(AgentStateType stateType)
    {
        State<AgentStateType, AgentContext> state = this.Create(stateType);
        return state;
    }
}
