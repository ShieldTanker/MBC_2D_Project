using System.Collections.Generic;

public class AgentIdleState : State<AgentStateType, AgentContext>
{
    public AgentIdleState(
        StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.AgentStat.IsBoost = false;
        _machine.Context.AnimCon.SetBool("IsBoost", false);
    }
}

public class AgentMoveState : State<AgentStateType, AgentContext>
{
    public AgentMoveState
        (StateMachine<AgentStateType, AgentContext> machine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
}

public class AgentJumpState : State<AgentStateType, AgentContext>
{
    public AgentJumpState
        (StateMachine<AgentStateType, AgentContext> stateMachine,
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = stateMachine;
        _transitions = transitions;
        _logics = logics;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        _machine.Context.AgentStat.IsJumping = true;
        _machine.Context.Move.Jump(_machine.Context.AgentStat.JumpForce);
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
        List<StateTransition<AgentStateType, AgentContext>> transitions,
        List<StateLogic<AgentContext>> logics)
    {
        _machine = machine;
        _transitions = transitions;
        _logics = logics;
    }
    public override void StateEnter()
    {
        base.StateEnter();
    }
}