using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    public Transform EffectTarget;
    public Transform AimTarget;

    public bool UseAim =true;
    public Vector3 BaseRot = new Vector3(0, 0, 0);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(BaseRot);
    }

    void Update()
    {
        if (!UseAim || AimTarget == null || EffectTarget == null) return;
        
        EffectTarget.position = AimTarget.position;
        EffectTarget.rotation = AimTarget.rotation;
    }
}
