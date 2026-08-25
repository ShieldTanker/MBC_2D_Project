public class BShoulderWeaponSelector : WeaponSelector
{
    public override void SetWeaponLoadoutData(WeaponData data)
    {
        LoadoutData.B_ShoulderWeaponData = data;
        _statusUI.UpdateUI();
    }
}