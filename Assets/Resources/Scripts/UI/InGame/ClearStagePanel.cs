using UnityEngine.UI;

public class ClearStagePanel : ResultPanelBase
{
    public Button ExitButton;

    public int idx;
    private void OnEnable()
    {
        AddClearStageAction();
    }

    private void OnDisable()
    {
        RemoveClearStageAction();
    }

    private void AddClearStageAction()
    {
        if (ReturnButton != null)
        {
            ReturnButton.Select();
            ReturnButton.onClick.AddListener(ReturnClick);
        }

        if (ExitButton != null)
            ExitButton.onClick.AddListener(ExitClick);
    }

    private void RemoveClearStageAction()
    {
        if (ReturnButton != null)
            ReturnButton.onClick.RemoveListener(ReturnClick);

        if (ExitButton != null)
            ExitButton.onClick.RemoveListener(ExitClick);
    }

    void ExitClick()
    {
        GameManager.Instance.ExitGame();
    }

    void ReturnClick()
    {
        SceneController.Instance.LoadScene(idx);
    }
}