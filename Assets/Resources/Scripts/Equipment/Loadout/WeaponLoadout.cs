using UnityEngine;

[DisallowMultipleComponent]
public class WeaponLoadout : Loadout
{
    public WeaponBase F_hand {  get; private set; }
    public WeaponBase B_hand {  get; private set; }
    public WeaponBase F_Shoulder {  get; private set; }
    public WeaponBase B_Shoulder { get; private set; }

    public WeaponData F_HandData { get; private set; }
    public WeaponData B_HandData { get; private set; }
    public WeaponData F_ShoulderData { get; private set; }
    public WeaponData B_ShoulderData { get; private set; }

    private void Awake()
    {
        F_hand = GetComponentInChildren<FHandWeapon>();
        B_hand = GetComponentInChildren<BHandWeapon>();

        F_Shoulder = GetComponentInChildren<FShoulderWeapon>();
        B_Shoulder = GetComponentInChildren<BShoulderWeapon>();
    }

    public override void SetLoadoutData(LoadoutData loadoutData)
    {
        if (loadoutData == null) return;

        F_HandData = loadoutData.F_HandWeaponData;
        B_HandData = loadoutData.B_HandWeaponData;
        F_ShoulderData = loadoutData.F_ShoulderWeaponData;
        B_ShoulderData = loadoutData.B_ShoulderWeaponData;

        if(F_HandData != null) F_hand.WeaponData = F_HandData;
        if(B_HandData != null) B_hand.WeaponData = B_HandData;
        if(F_ShoulderData != null) F_Shoulder.WeaponData = F_ShoulderData;
        if(B_ShoulderData != null) B_Shoulder.WeaponData = B_ShoulderData;
    }
}
