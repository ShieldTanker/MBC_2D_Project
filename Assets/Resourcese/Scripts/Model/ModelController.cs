using UnityEngine;

public class ModelController : MonoBehaviour
{
    public Animator Anim;

    [Header("손 IK 위치")]
    public Transform F_HandIKTarget;
    public Transform B_HandIKTarget;

    [Header("발 IK 위치")]
    public Transform F_FootIKTarget;
    public Transform B_FootIKTarget;

    [Header("손 무장 조준용 본 위치")]
    public Transform F_Shoudler;
    public Transform B_Shoudler;
    [Header("어깨 무장 조준용 본 위치")]
    public Transform BackWeaponPos;
}
