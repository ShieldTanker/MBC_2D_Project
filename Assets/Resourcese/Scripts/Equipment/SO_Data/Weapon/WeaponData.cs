using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("모델 관련")]
    public WeaponModel Model;
    public Vector3 Offset;

    [Header("스탯 관련")]
    public int Damage;
    public float FireRate;

    public RecoilData RecoilData;
}
