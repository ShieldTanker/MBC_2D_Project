public class AgentIdleToMove : StateTransition<AgentStateType, AgentContext>
{
    private AgentStateType _nextState = AgentStateType.Move;
    private AgentContext _context;

    public AgentIdleToMove(AgentContext context) : base(context) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        //if (_context.Movement.GetMoveInput() >= Vector2.zero)
        //{
        //    임시 예시용
        //}

        return true;    // 임시용
    }

    public override AgentStateType ChangeNextState() { return _nextState; }
}
