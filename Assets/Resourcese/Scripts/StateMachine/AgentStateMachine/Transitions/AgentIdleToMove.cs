using UnityEngine;

public class AgentIdleToMove : StateTransition<AgentStateType, AgentContext>
{
    private AgentStateType _nextState = AgentStateType.Move;

    public AgentIdleToMove(AgentContext context) : base(context) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude > 0)
            return true;
        
        return false;
    }

    public override AgentStateType ChangeNextState() { return _nextState; }
}
