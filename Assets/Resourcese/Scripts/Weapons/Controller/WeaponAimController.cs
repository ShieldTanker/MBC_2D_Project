using UnityEngine;

public enum WeaponAimType
{
    F_Hand,
    B_Hand,
    F_Shoulder,
    B_Shoulder,
}

public class WeaponAimController : MonoBehaviour
{
    [Header("손 무장")]
    public WeaponAimAnchor F_HandAnchor;
    public WeaponAimAnchor B_HandAnchor;

    [Header("어깨 무장")]
    public WeaponAimAnchor F_ShoulderAnchor;
    public WeaponAimAnchor B_ShoulderAnchor;

    public void SetAimTarget(Transform target)
    {
        F_HandAnchor?.SetTarget(target);
        B_HandAnchor?.SetTarget(target);
        F_ShoulderAnchor?.SetTarget(target);
        B_ShoulderAnchor?.SetTarget(target);
    }
}