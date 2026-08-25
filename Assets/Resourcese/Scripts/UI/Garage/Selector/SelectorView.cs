using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class SelectorView : MonoBehaviour
{
    public PartSelectorController[] selectors;

    public Button CloseButton;

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
