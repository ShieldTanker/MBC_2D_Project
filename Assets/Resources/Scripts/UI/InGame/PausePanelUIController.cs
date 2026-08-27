using UnityEngine;

public class PausePanelUIController : MonoBehaviour
{
    public GameObject PausePanel;
    bool toggle = false;

    private void Start()
    {
        if(PausePanel.activeInHierarchy)
            PausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerUIEventBus.Subscribe(PlayerUIEventType.EscapeInput, PausePanelSet);
    }

    private void OnDisable()
    {
        PlayerUIEventBus.Unsubscribe(PlayerUIEventType.EscapeInput, PausePanelSet);
    }

    public void PausePanelSet()
    {
        toggle = !toggle;
        PausePanel.SetActive(toggle);
        if (toggle)
            GameManager.Instance.PauseGame();
        else
            GameManager.Instance.ResumeGame();
    }
}
