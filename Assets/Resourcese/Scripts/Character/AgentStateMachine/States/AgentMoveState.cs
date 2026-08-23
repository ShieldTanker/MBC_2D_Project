using System.Collections.Generic;
using UnityEngine;

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
        _context.Move.Acceleration = _context.AgentStat.IsBoost ?
            _context.AgentStat.BoostAcceleration : _context.AgentStat.Acceleration;
        _context.Move.Deceleration = _context.AgentStat.IsBoost ?
            _context.AgentStat.BoostDeceleration : _context.AgentStat.Deceleration;
        _context.Move.IsBoosting = _context.AgentStat.IsBoost;
    }

    public override void StateUpdate(float deltaTime)
    {
        Vector2 input = _context.MoveInput.MoveInput;

        _context.Move.MoveInput(input);
        _context.Move.IsBoosting = _context.AgentStat.IsBoost;
        _context.ModelCon.Anim.SetFloat("MoveX", input.x);

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}