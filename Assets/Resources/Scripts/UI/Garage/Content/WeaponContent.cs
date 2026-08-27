public class WeaponContent : PartContent
{
    public WeaponData _weaponData;
    WeaponSelector _selector;
    private void Awake()
    {
        _selector = controller as WeaponSelector;
        _button.onClick.AddListener(OnWeaponLoadoutClick);
        _text.text = _weaponData != null ? _weaponData.WeaponName : "None";
    }

    public void OnWeaponLoadoutClick()
    {
        _selector.SetWeaponLoadoutData(_weaponData);
    }
}