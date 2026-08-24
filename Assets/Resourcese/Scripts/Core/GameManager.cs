using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameStateMachine gameStateMachine;
    GameContext _context = new GameContext();
    public GameContext Context {  get { return _context; } }

    protected override void OnAwake()
    {
        base.OnAwake();
        gameStateMachine = new GameStateMachine(_context);
    }

    public void ExitGame() { Debug.Log("게임 종료"); }
}