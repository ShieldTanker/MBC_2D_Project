using System;
using System.Collections.Generic;

public static class AgentStateFactory
{
    /// <summary>
    /// 호출하는 상태머신을 매개변수로 원하는 타입의 상태 초기화
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public static State<AgentStateType, AgentContext> Create(this AgentStateMachine machine, AgentStateType stateType)
    {
        State<AgentStateType, AgentContext> state = null;
        List<StateTransition<AgentStateType, AgentContext>> transitions = new();
        List<StateLogic<AgentContext>> logics = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        // AgentStateMachine 에서 Context를 받음으로 AgentContext형식
        switch (stateType)
        {
            case AgentStateType.Idle:
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));
                transitions.Add(new AgentStateToJump(machine.Context, AgentStateType.Jump));
                // Todo: IdleToJump 추가할것
                // Todo: IdleToOnAir 추가할것
                // Todo: IdleToDied 추가할것
                // 닷지는 입력방향이 있어야함으로 Idle에서는 못함

                logics.Add(new AgentIdleStateLogic(machine.Context));
                state = new AgentIdleState(machine, transitions, logics);
                break;

            case AgentStateType.Move:
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToJump(machine.Context, AgentStateType.Jump));
                // Todo: MoveToJump 추가할것
                // Todo: MoveToOnAir 추가할것
                // Todo: MoveToDodge 추가할것
                // Todo: MoveToDied 추가할것

                logics.Add(new AgentMoveStateLogic(machine.Context));
                state = new AgentMoveState(machine, transitions, logics);
                break;

            case AgentStateType.Jump:
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));
                transitions.Add(new AgentStateToOnAir(machine.Context, AgentStateType.OnAir));
                transitions.Add(new AgentStateToLanding(machine.Context, AgentStateType.Landing));
                // Todo: JumpToDied 추가할것
                // Todo: jumpToDodge 추가할것

                // 점프중에는 이동은 막지만 회피는 가능
                logics.Add(new AgentJumpStateLogic(machine.Context));
                state = new AgentJumpState(machine, transitions, logics);
                break;

            case AgentStateType.OnAir:
                transitions.Add(new AgentStateToIdle(machine.Context, AgentStateType.Idle));
                transitions.Add(new AgentStateToMove(machine.Context, AgentStateType.Move));
                transitions.Add(new AgentStateToLanding(machine.Context, AgentStateType.Landing));
                

                state = new AgentOnAirState(machine, transitions, logics);
                break;
        }

        return state;
    }
}
