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

public class AgentStateToLanding : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToLanding(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        // 부스트 상태가 아니고 땅에 닿아있으면

        if (!_context.AgentStat.IsBoost && _context.Move.IsGround)
        {
            if (_context.MoveInput.MoveInput.sqrMagnitude > 0.001f)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public override void Clear() { }
}

public class AgentStateToDodge : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToDodge(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (!_context.AgentFlag.IsJumping && _context.Move.IsGround)
        {
            return true;
        }

        return false;
    }

    public override void Clear() { }
}