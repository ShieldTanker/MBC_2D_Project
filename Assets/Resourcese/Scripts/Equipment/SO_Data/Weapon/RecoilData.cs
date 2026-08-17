using UnityEngine;

[CreateAssetMenu(fileName = "RecoilData", menuName = "Scriptable Objects/RecoilData")]
public class RecoilData : ScriptableObject
{
    public float KickBack;
    public float KickPitchAngle;

    // 반동 위치까지 도달하는데 속도
    public float KickSpeed;
    // 반동 회복 속도
    public float RecoverySpeed;

    // 반동 지속시간
    public float RecoilDuration;
}
