public class FShoulderWeaponSelector : WeaponSelector
{
    public override void SetWeaponLoadoutData(WeaponData data)
    {
        LoadoutData.F_ShoulderWeaponData = data;
        _statusUI.UpdateUI();
    }
}
