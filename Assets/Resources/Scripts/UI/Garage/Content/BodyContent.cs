using UnityEngine.UI;

public class BodyContent : PartContent
{
    public PartDataBase _partData;
    BodyPartSelector _selector;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnBodyLoadoutClick);
        _text.text = _partData != null ?_partData.PartName : "None";
        _selector = controller as BodyPartSelector;
    }

    public void OnBodyLoadoutClick()
    {
        _selector.SetBodyLoadoutData(_partData);
    }
}
