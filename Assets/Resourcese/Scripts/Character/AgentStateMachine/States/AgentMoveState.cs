using System.Collections.Generic;
using UnityEngine;

public class AgentMoveState : State<AgentStateType, AgentContext>
{
    Movement2D _move;
    IMoveInput2D _input;
    ModelController _model;
    AgentStat _stat;
    AgentFlag _flag;

    public AgentMoveState(StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _move = _machine.Context.Move;
        _input = _machine.Context.MoveInput;
        _stat = _machine.Context.AgentStat;
        _flag = _machine.Context.AgentFlag;
        _model = _machine.Context.ModelCon;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        BoostSet();
    }

    public override void StateUpdate(float deltaTime)
    {
        BoostSet();

        _move.MoveInput(_input.MoveInput);
        _model.Anim.SetBool("IsBoost", _stat.IsBoost);
        _model.Anim.SetFloat("MoveX", _input.MoveInput.x * (int)_stat.CurrentDirection);

        base.StateUpdate(deltaTime);
    }

    void BoostSet()
    {
        _move.IsBoosting = _stat.IsBoost;

        _move.MoveSpeed = _stat.IsBoost ? _stat.BoostSpeed : _stat.MoveSpeed;
        _move.Acceleration = _stat.IsBoost ? _stat.BoostAcceleration : _stat.Acceleration;
        _move.Deceleration = _stat.IsBoost ? _stat.BoostDeceleration : _stat.Deceleration;

    }
}