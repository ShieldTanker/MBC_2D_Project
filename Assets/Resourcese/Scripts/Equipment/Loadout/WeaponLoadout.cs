using UnityEngine;

[DisallowMultipleComponent]
public class WeaponLoadout : Loadout
{
    public WeaponBase _f_hand;
    public WeaponBase _b_hand;
    public WeaponBase _f_Shoulder;
    public WeaponBase _b_Shoulder;

    WeaponData _f_HandData;
    WeaponData _b_HandData;
    WeaponData _f_ShoulderData;
    WeaponData _b_ShoulderData;

    private void Awake()
    {
        _f_hand = GetComponentInChildren<FHandWeapon>();
        _b_hand = GetComponentInChildren<BHandWeapon>();

        _f_Shoulder = GetComponentInChildren<FShoulderWeapon>();
        _b_Shoulder = GetComponentInChildren<BShoulderWeapon>();
    }

    public override void SetLoadoutData(LoadoutData loadoutData)
    {
        if (loadoutData == null) return;

        _f_HandData = loadoutData.F_HandWeaponData;
        _b_HandData = loadoutData.B_HandWeaponData;
        _f_ShoulderData = loadoutData.F_ShoudlerWeaponData;
        _b_ShoulderData = loadoutData.B_ShoulderWeaponData;

        if(_f_HandData != null)
            _f_hand.WeaponData = _f_HandData;
        if(_b_HandData != null)
            _b_hand.WeaponData = _b_HandData;
        if(_f_ShoulderData != null)
            _f_Shoulder.WeaponData = _f_ShoulderData;
        if(_b_ShoulderData == null)
            _b_Shoulder.WeaponData = _b_ShoulderData;
    }
}
