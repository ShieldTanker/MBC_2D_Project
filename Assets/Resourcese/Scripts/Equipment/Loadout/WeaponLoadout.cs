using UnityEngine;

[DisallowMultipleComponent]
public class WeaponLoadout : Loadout
{
    public WeaponBase F_hand {  get; private set; }
    public WeaponBase B_hand {  get; private set; }
    public WeaponBase F_Shoulder {  get; private set; }
    public WeaponBase B_Shoulder { get; private set; }

    WeaponData _f_HandData;
    WeaponData _b_HandData;
    WeaponData _f_ShoulderData;
    WeaponData _b_ShoulderData;

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

        _f_HandData = loadoutData.F_HandWeaponData;
        _b_HandData = loadoutData.B_HandWeaponData;
        _f_ShoulderData = loadoutData.F_ShoudlerWeaponData;
        _b_ShoulderData = loadoutData.B_ShoulderWeaponData;

        if(_f_HandData != null) F_hand.WeaponData = _f_HandData;
        if(_b_HandData != null) B_hand.WeaponData = _b_HandData;
        if(_f_ShoulderData != null) F_Shoulder.WeaponData = _f_ShoulderData;
        if(_b_ShoulderData == null) B_Shoulder.WeaponData = _b_ShoulderData;
    }
}
