using UnityEngine;

public class AgentStateToIdle : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToIdle(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude <= 0
            && _context.Move.IsGround)
            return true;

        return false;
    }
}

public class AgentStateToMove : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToMove(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (_context.InputCon.MoveInput.sqrMagnitude > 0
            && _context.Move.IsGround)
            return true;

        return false;
    }
}

public class AgentStateToJump : StateTransition<AgentStateType, AgentContext>
{
    float time = 0f;
    public AgentStateToJump(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (!_context.Move.IsGround)
        {
            time = 0f;
            return false;
        } 

        if (_context.JumpInput.JumpInput)
        {
            time += deltaTime;
            if(time >= _context.AgentStat.JumpDelay)
            {
                time = 0f;
                return true;
            }
        }
        else
        {
            time = 0f;
        }

        return false;
    }
}

public class AgentStateToOnAir : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToOnAir(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (!_context.AgentStat.IsJumping && !_context.Move.IsGround)
        {
            return true;
        }

        return false;
    }
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
            return true;
        }

        return false;
    }
}

public class AgentStateToDodge : StateTransition<AgentStateType, AgentContext>
{
    public AgentStateToDodge(AgentContext context, AgentStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime)
    {
        if (!_context.AgentStat.IsJumping && _context.Move.IsGround)
        {
            return true;
        }

        return false;
    }
}