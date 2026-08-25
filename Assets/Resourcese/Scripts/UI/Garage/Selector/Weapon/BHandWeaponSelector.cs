public class BHandWeaponSelector : WeaponSelector
{
    public override void SetWeaponLoadoutData(WeaponData data)
    {
        LoadoutData.B_HandWeaponData = data;
        _statusUI.UpdateUI();
    }
}
