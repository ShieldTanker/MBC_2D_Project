using System.Collections.Generic;
using UnityEngine;

public class AgentIdleState : State<AgentStateType, AgentContext>
{
    public AgentIdleState(StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
    }
    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.AgentStat.IsBoost = false;
        _machine.Context.AnimCon.SetBool("IsBoost", false);
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}

public class AgentMoveState : State<AgentStateType, AgentContext>
{
    AgentContext _context;

    public AgentMoveState(StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
        Vector2 input = _context.MoveInput.MoveInput;

        _context.Move.MoveInput(input);
        _context.AnimCon.SetFlaot("MoveX", input.x);

    }

    public override void StateExit()
    {
        base.StateExit();
    }
}

public class AgentJumpState : State<AgentStateType, AgentContext>
{
    AgentContext _context;
    float time = 0f;

    public AgentJumpState(StateMachine<AgentStateType, AgentContext> stateMachine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = stateMachine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.AgentStat.IsJumping = true;
        _machine.Context.Move.Jump(_machine.Context.AgentStat.JumpForce);
    }

    public override void StateUpdate(float deltaTime)
    {
        if (time >= _context.AgentStat.JumpDuration)
        {
            time = 0;
            _context.AgentStat.IsJumping = false;
        }

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();

        _machine.Context.AgentStat.IsJumping = false;
    }
}

public class AgentOnAirState : State<AgentStateType, AgentContext>
{
    public AgentOnAirState(
        StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
    }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}