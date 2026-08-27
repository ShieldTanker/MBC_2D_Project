public class GamePauseToPlay : StateTransition<GameStateType, GameContext>
{
    public GamePauseToPlay(GameContext context, GameStateType stateType)
        : base(context, stateType) { }

    public override bool CheckStateTransit(float deltaTime) { return true; } // 임시용

    public override void Clear() { }
}