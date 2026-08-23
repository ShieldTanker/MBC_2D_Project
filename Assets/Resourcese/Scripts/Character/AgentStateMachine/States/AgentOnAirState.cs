using System.Collections.Generic;

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
        _context.Move.MoveSpeed =
            _context.AgentStat.IsBoost ? _context.AgentStat.BoostSpeed : _context.AgentStat.MoveSpeed;
        _context.Move.Acceleration =
            _context.AgentStat.IsBoost ? _context.AgentStat.BoostAcceleration : _context.AgentStat.Acceleration;
        _context.Move.Deceleration =
            _context.AgentStat.IsBoost ? _context.AgentStat.BoostDeceleration : _context.AgentStat.Deceleration;
        _context.Move.IsBoosting = _context.AgentStat.IsBoost;
    }

    public override void StateUpdate(float deltaTime)
    {
        _context.Move.MoveInput(_context.MoveInput.MoveInput);
        _context.Move.IsBoosting = _context.AgentStat.IsBoost;
        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}