public class WeaponSelector : PartSelectorController
{
    WeaponContent[] weapons;
    public override void OnAwake()
    {
        weapons = GetComponentsInChildren<WeaponContent>();
        foreach (var weapon in weapons)
        {
            weapon.controller = this;
        }
    }

    public virtual void SetWeaponLoadoutData(WeaponData data) { }
}
