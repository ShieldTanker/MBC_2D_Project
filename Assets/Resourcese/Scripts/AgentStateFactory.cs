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
        List<StateTransition> transitions = new();
        List<StateLogic> states = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        switch (stateType)
        {
            case AgentStateType.Idle:
                break;

            case AgentStateType.Move:
                break;
        }

        return state;
    }
}

public static class GameStateFactory
{
    /// <summary>
    /// 호출하는 상태머신을 매개변수로 원하는 타입의 상태 초기화
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public static State<GameStateType, GameContext> InitState(this GameStateMachine machine, GameStateType stateType)
    {
        State<GameStateType, GameContext> state = null;
        List<StateTransition> transitions = new();
        List<StateLogic> states = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        switch (stateType)
        {
            case GameStateType.Play:
                break;

            case GameStateType.Pause:
                break;
        }

        return state;
    }
}