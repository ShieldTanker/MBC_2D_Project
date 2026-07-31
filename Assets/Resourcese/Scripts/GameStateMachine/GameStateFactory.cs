using System.Collections.Generic;

public static class GameStateFactory
{
    /// <summary>
    /// 호출하는 상태머신을 매개변수로 원하는 타입의 상태 초기화
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public static State<GameStateType, GameContext> Create(this GameStateMachine machine, GameStateType stateType)
    {
        State<GameStateType,GameContext> state = null;
        List<StateTransition<GameStateType, GameContext>> transitions = new();
        List<StateLogic<GameContext>> states = new();

        // TODO : 각 상태에서 필요한 것들 초기화
        switch (stateType)
        {
            case GameStateType.Play:
                break;

            case GameStateType.Pause:
                transitions.Add(new GamePauseToPlay(machine.Context as GameContext));
                break;
        }

        return state;
    }
}