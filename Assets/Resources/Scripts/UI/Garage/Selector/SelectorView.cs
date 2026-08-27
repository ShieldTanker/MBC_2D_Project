using UnityEngine;
using UnityEngine.UI;

public class SelectorView : MonoBehaviour
{
    public PartSelectorController[] selectors;

    public Button CloseButton;
    public Button GarageStartButton;

    private void Awake()
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (CloseButton != null)
            CloseButton.Select();
    }

    private void OnDisable()
    {
        if(GarageStartButton != null)
            GarageStartButton.Select();
    }

    private void Start()
    {
        CloseButton.onClick.AddListener(Close);
    }

    public void SelectController(int index)
    {
        CloseAll();

        selectors[index].gameObject.SetActive(true);
    }

    public void CloseAll()
    {
        foreach (var selector in selectors)
        {
            selector.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
