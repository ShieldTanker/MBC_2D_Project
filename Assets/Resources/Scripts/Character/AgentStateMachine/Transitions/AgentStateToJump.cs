public class AgentStateToJump : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToJump(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return _context.Move.IsGround && _context.JumpInput.JumpInput;
    }

    public override void Clear() { }
}
