using UnityEngine;

public class AgentStateToIdle : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToIdle(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude <= 0
            && _context.Move.IsGround
            && !_context.AgentFlag.IsJumping)
            return true;

        return false;
    }

    public override void Clear() { }
}

public class AgentStateToDie : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToDie(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        return !_context.AgentStat.IsAlive;
    }

    public override void Clear() { }
}