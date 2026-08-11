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

    public void SetAnchorPosition(Transform transform, WeaponAimType type)
    {
        switch (type)
        {
            case WeaponAimType.F_Hand:
                if(F_HandAnchor != null)
                    F_HandAnchor.PosTarget = transform;
                break;
            case WeaponAimType.B_Hand:
                if(B_HandAnchor != null)
                    B_HandAnchor.PosTarget = transform;
                break;
            case WeaponAimType.F_Shoulder:
                if(F_ShoulderAnchor != null)
                    F_ShoulderAnchor.PosTarget = transform;
                break;
            case WeaponAimType.B_Shoulder:
                if(B_ShoulderAnchor != null)
                    B_ShoulderAnchor.PosTarget = transform;
                break;
            default: 
                break;
        }
    }

    public void SetEffectTarget(Transform transform, WeaponAimType type)
    {
        switch (type)
        {
            case WeaponAimType.F_Hand:
                if(F_HandAnchor != null)
                    F_HandAnchor.EffectTarget = transform;
                break;
            case WeaponAimType.B_Hand:
                if (B_HandAnchor != null)
                    B_HandAnchor.EffectTarget = transform;
                break;
            case WeaponAimType.F_Shoulder:
                if (F_ShoulderAnchor != null)
                    F_ShoulderAnchor.EffectTarget = transform;
                break;
            case WeaponAimType.B_Shoulder:
                if (B_ShoulderAnchor != null)
                    B_ShoulderAnchor.EffectTarget = transform;
                break;
        }
    }
}