public class AgentStateToOnAir : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToOnAir(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (!_context.AgentFlag.IsJumping && !_context.Move.IsGround)
        {
            return true;
        }

        return false;
    }

    public override void Clear() { }
}
