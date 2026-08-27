using UnityEngine;

public class ResultCanvas : MonoBehaviour
{
    public ResultPanelBase ClearPanel;
    public ResultPanelBase RestartPanel;

    private void Start()
    {
        if(ClearPanel != null)
            if(ClearPanel.gameObject.activeInHierarchy)
                ClearPanel.gameObject.SetActive(false);

        if(RestartPanel != null)
            if(RestartPanel.gameObject.activeInHierarchy)
                RestartPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerBattleUIEventBus.Subscribe(PlayerBattleUIEventType.PlayerDie, OnGameOver);
        StageUIEventBus.Subscribe(StageEventType.ClearStage, OnStageClear);
    }

    private void OnDisable()
    {
        PlayerBattleUIEventBus.Unsubscribe(PlayerBattleUIEventType.PlayerDie, OnGameOver);
        StageUIEventBus.Unsubscribe(StageEventType.ClearStage, OnStageClear);
    }

    void OnStageClear()
    {
        ClearPanel.gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    void OnGameOver(Player _)
    {
        RestartPanel.gameObject.SetActive(true);
    }
}