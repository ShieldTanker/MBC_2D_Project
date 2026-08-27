public class WeaponSelector : PartSelectorController
{
    WeaponContent[] weapons;
    public override void OnAwake()
    {
        weapons = GetComponentsInChildren<WeaponContent>();

        if (weapons.Length > 0)
        {
            foreach (var weapon in weapons)
                weapon.controller = this;
        }
    }
    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            if (weapons.Length > 0)
                weapons[0]._button.Select();
        }
    }

    public virtual void SetWeaponLoadoutData(WeaponData data) { }
}
