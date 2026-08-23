using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentJumpState : State<AgentStateType, AgentContext>
{
    ModelController _model;
    Movement2D _move;
    IMoveInput2D _input;
    AgentFlag _flag;
    AgentStat _stat;

    float time = 0f;
    bool _jumped = false;
    float _jumpDirX = 0f;
    float _jumpDuration = 0f;

    public AgentJumpState(StateMachine<AgentStateType, AgentContext> stateMachine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = stateMachine;
        _transitions = transitions;

        _model = _machine.Context.ModelCon;
        _move = _machine.Context.Move;
        _flag = _machine.Context.AgentFlag;
        _stat = _machine.Context.AgentStat;
        _input = _machine.Context.MoveInput;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _flag.IsJumping = true;
        _jumped = false;

        _move.MoveInput(Vector2.zero);
        _model.Anim.Play("Jump", 0);
    }

    public override void StateUpdate(float deltaTime)
    {
        // 최소 점프 시간
        time += deltaTime;
        if (!_jumped)
        {
            if (time >= _stat.JumpDelay)
            {
                _move.MoveInput(_input.MoveInput);
                _machine.Context.Move.Jump(_machine.Context.AgentStat.JumpHeight);
                _jumped = true;
                _jumpDuration = _move.CalculateJumpDuration(_stat.JumpHeight);

                time -= _stat.JumpDelay;
            }
        }
        else
        {
            _move.MoveInput(_input.MoveInput);

            // 최대 점프 시간 - 방향까지 반영해서 실제 정점 도달 시간에 맞춤
            if (time >= _jumpDuration)
            {
                _flag.IsJumping = false;
            }
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