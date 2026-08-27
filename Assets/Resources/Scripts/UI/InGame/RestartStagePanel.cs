using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartStagePanel : ResultPanelBase
{
    public Button RestartButton;
    public int idx;

    private void OnEnable()
    {
        Cursor.visible = true;

        if(ReturnButton != null)
        {
            ReturnButton.onClick.AddListener(ReturnGame);
        }
        if (RestartButton != null)
        {
            RestartButton.Select();
            RestartButton.onClick.AddListener(RestartGame);
        }
    }

    private void OnDisable()
    {
        if (ReturnButton != null)
        {
            ReturnButton.onClick.RemoveListener(ReturnGame);
        }
        if (RestartButton != null)
            RestartButton.onClick.RemoveListener(RestartGame);
    }

    void ReturnGame()
    {
        SceneController.Instance.LoadScene(idx);
    }

    void RestartGame()
    {
        SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
