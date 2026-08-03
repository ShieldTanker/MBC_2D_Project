using UnityEngine;

public class AgentMoveToIdle : StateTransition<AgentStateType, AgentContext>
{
    private AgentStateType nextType = AgentStateType.Idle;

    public AgentMoveToIdle(AgentContext context) : base(context) { }

    public override AgentStateType ChangeNextState()
    {
        return nextType;
    }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude <= 0)
            return true;

        return false;
    }
}
