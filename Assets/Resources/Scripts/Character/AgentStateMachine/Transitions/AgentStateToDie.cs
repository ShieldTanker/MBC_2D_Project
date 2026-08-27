public class AgentStateToDie : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToDie(AgentContext context, AgentStateType stateType) : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return !_context.AgentStat.IsAlive;
    }

    public override void Clear() { }
}