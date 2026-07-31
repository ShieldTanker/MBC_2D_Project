public class GameStateMachine : StateMachine<GameStateType, GameContext>
{
    public GameStateMachine(GameContext context) { Context = context; }

    protected override State<GameStateType, GameContext> CreateState(GameStateType stateType)
    {
        State<GameStateType, GameContext> state = this.Create(stateType);
        return state;
    }
}