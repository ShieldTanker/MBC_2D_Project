public class FHandWeaponSelector : WeaponSelector
{
    public override void SetWeaponLoadoutData(WeaponData data)
    {
        LoadoutData.F_HandWeaponData = data;
        _statusUI.UpdateUI();
    }
}
