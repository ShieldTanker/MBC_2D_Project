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
        // 이동 정지
        _machine.Context.AgentStat.IsBoost = false;
        _machine.Context.Move.MoveInput(Vector2.zero);
        // 애니메이션 처리
        _machine.Context.AnimCon.SetFlaot("MoveX", 0f);
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
        _context.Move.MoveSpeed = _context.AgentStat.IsBoost ?
            _context.AgentStat.BoostSpeed : _context.AgentStat.MoveSpeed; 
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
    bool _jumped = false;

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
        _machine.Context.AgentFlag.IsJumping = true;
        _jumped = false;
        _machine.Context.AnimCon.model.Anim.Play("Jump", 0);
    }

    public override void StateUpdate(float deltaTime)
    {
        // 최소 점프 시간
        time += deltaTime;
        if (time >= _context.AgentStat.JumpDelay)
        {
            if (!_jumped)
            {
                _machine.Context.Move.Jump(_machine.Context.AgentStat.JumpForce);
                _jumped = true;
            }
        }
        // 최대 점프 시간
        if (time >= _context.AgentStat.JumpDuration)
        {
            _context.AgentFlag.IsJumping = false;
        }

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
        time = 0;
        _machine.Context.AgentFlag.IsJumping = false;
    }
}

public class AgentOnAirState : State<AgentStateType, AgentContext>
{
    AgentContext _context;
    public AgentOnAirState(StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _context = _machine.Context;
        _context.Move.MoveSpeed = _context.AgentStat.IsBoost ?
    _context.AgentStat.BoostSpeed : _context.AgentStat.MoveSpeed;
    }

    public override void StateUpdate(float deltaTime)
    {
        if(_context.MoveInput.MoveInput.sqrMagnitude >= 0.01f)
        {
            _context.Move.MoveInput(_context.MoveInput.MoveInput);
        }
        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}