using Unity.VisualScripting;
using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    public Transform PosTarget;
    public Transform EffectTarget;
    public Transform AimTarget;

    public bool UseAim =true;

    private Vector3 _aimOffset = new Vector3(3, -0.5f, 0);
    public Vector3 AimOffset
    {
        get { return _aimOffset; }
        set 
        {
            _aimOffset = value;
            AimTarget.localPosition = _aimOffset;
        }
    }

    private void Start()
    {
        if (AimTarget == null) return;
        AimTarget.localPosition = _aimOffset;
    }

    void Update()
    {
        if (PosTarget == null) return;
        // 타겟 위치를 중심으로 회전시켜 조준할것
        transform.position = PosTarget.position;

        // 대상 IK의 위치와 회전을 맞춤
        if (!UseAim) return;
        if (AimTarget == null || EffectTarget == null) return;
        EffectTarget.position = AimTarget.position;
        EffectTarget.rotation = AimTarget.rotation;
    }
}
