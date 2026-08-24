using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;

    private void OnEnable()
    {
        StartButton.onClick.AddListener(StartButtonPressed);
        ExitButton.onClick.AddListener(ExitButtonPressed);
    }

    private void OnDisable()
    {
        StartButton.onClick.RemoveListener(StartButtonPressed);
        ExitButton.onClick.RemoveListener(ExitButtonPressed);
    }

    public void ExitButtonPressed()
    {
        GameManager.Instance.ExitGame();
    }

    public void StartButtonPressed()
    {
        SceneController.Instance.LoadScene(1);
    }
}