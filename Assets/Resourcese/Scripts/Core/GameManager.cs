using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameStateMachine gameStateMachine;
    GameContext _context = new GameContext();
    public GameContext Context { get { return _context; } }

    protected override void OnAwake()
    {
        base.OnAwake();
        gameStateMachine = new GameStateMachine(_context);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}