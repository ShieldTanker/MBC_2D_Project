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
        _machine.Context.ModelCon.Anim.SetFloat("MoveX", 0f);
        _machine.Context.ModelCon.Anim.SetBool("IsBoost", false);
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