using System.Collections.Generic;

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

        PlayerBattleUIEventBus.Publish(PlayerBattleUIEventType.PlayerDie, _machine.Context.Player);
    }
}