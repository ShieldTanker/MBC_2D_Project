using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("모델 관련")]
    public WeaponModel Model;
    public Vector3 Offset;

    [Header("스탯 관련")]
    public WeaponType WeaponType;
    public WeaponFireMode FireMode;

    public int Damage;
    public float FireRateSecond;
    public float Range;

    public float MaxAimTime = 1f;
    public float MaxReloadDuration;

    [Header("탄약 정보")]
    public GameObject BulletModel;
    public float BulletSpeed;
    public int MaxCapacity;
    public int MaxAmmo;

    [Header("반동 데이터")]
    public RecoilData RecoilData;
}
