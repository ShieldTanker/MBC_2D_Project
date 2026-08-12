using UnityEngine;

public class AgentIdleStateLogic : StateLogic<AgentContext>
{
    public AgentIdleStateLogic(AgentContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {

        // TODO : _context를 이용해 로직 구현
    }
}

public class AgentMoveStateLogic : StateLogic<AgentContext>
{
    public AgentMoveStateLogic(AgentContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        Vector2 input = _context.MoveInput.MoveInput;

        _context.Move.MoveInput(input);
        _context.AnimCon.SetFlaot("MoveX", input.x);

    }
}

public class AgentJumpStateLogic : StateLogic<AgentContext>
{
    float time = 0f;
    public AgentJumpStateLogic(AgentContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        if (time >= _context.AgentStat.JumpDuration)
        {
            time = 0;
            _context.AgentStat.IsJumping = false;
        }
    }
}

