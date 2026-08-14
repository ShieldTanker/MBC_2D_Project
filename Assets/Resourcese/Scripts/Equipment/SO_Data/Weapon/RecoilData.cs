using UnityEngine;

[CreateAssetMenu(fileName = "RecoilData", menuName = "Scriptable Objects/RecoilData")]
public class RecoilData : ScriptableObject
{
    public float KickBack;
    public float KickPitchAngle;

    public float KickSpeed;
    public float RecoverySspeed;
}
