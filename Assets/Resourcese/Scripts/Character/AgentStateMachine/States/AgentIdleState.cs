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
}

public class AgentDieState : State<AgentStateType, AgentContext>
{
    AgentStat _stat;
    ModelController _model;
    WeaponController _weaponCon;
    public AgentDieState(StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _stat = _machine.Context.AgentStat;
        _model = _machine.Context.ModelCon;
        _weaponCon = _machine.Context.WeaponController;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _stat.IsAlive = false;
        _stat.CurrentHp = 0;
        _model.Anim.SetTrigger("Die");
        _weaponCon.SetAlive(false);

        PlayerUIEventBus.Publish(PlayerBattleUIEventType.PlayerDie, _machine.Context.Player);
    }
}