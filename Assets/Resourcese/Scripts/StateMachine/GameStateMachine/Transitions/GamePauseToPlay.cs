public class GamePauseToPlay : StateTransition<GameStateType, GameContext>
{
    private GameStateType _nextState = GameStateType.Play;

    public GamePauseToPlay(GameContext context) : base(context) { }

    public override bool CheckStateTransit(float deltaTime) { return true; } // 임시용

    public override GameStateType ChangeNextState() { return _nextState; }
}