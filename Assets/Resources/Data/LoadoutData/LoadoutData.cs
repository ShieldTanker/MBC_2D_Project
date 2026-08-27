using UnityEngine;

[CreateAssetMenu(fileName = "LoadoutData", menuName = "Scriptable Objects/LoadoutData")]
public class LoadoutData : ScriptableObject
{
    [Header("파츠 정보")]
    public HeadPartData HeadPartData;
    public BodyPartData BodyPartData;
    public LegsPartData LegsPartData;
    public ArmsPartData ArmsPartData;

    [Header("무장 정보")]
    public WeaponData F_HandWeaponData;
    public WeaponData B_HandWeaponData;
    public WeaponData F_ShoulderWeaponData;
    public WeaponData B_ShoulderWeaponData;

}
