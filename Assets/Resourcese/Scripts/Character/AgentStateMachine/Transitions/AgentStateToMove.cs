public class AgentStateToMove : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToMove(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude > 0
            && _context.Move.IsGround
            && !_context.AgentFlag.IsJumping)
            return true;

        return false;
    }

    public override void Clear() { }
}
