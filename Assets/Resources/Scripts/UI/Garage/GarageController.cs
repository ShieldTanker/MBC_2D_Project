using UnityEngine;
using UnityEngine.UI;

public class GarageController : MonoBehaviour
{
    public Button StartButton;
    public Button AssembleButton;
    public Button ExitButton;

    public GameObject SettingPannel;
    public GameObject AssemblePannel;

    private void OnEnable()
    {
        StartButton.Select();
        StartButton.onClick.AddListener(StartGame);
        AssembleButton.onClick.AddListener(AssemblePanelOpen);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        StartButton.onClick.RemoveListener(StartGame);
        AssembleButton.onClick.RemoveListener(AssemblePanelOpen);
        ExitButton.onClick.RemoveListener(ExitGame);
    }

    public void StartGame()
    {
        SceneController.Instance.LoadScene(2);
    }

    public void AssemblePanelOpen()
    {
        AssemblePannel.SetActive(true);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
