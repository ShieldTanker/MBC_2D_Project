using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    public WeaponHolder AimTarget;
    public Transform EffectTarget;

    public bool UseAim = true;
    public Vector3 BaseRot = new Vector3(0, 0, 0);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(BaseRot);
    }

    void Update()
    {
        if (!UseAim || AimTarget == null) return;

        EffectTarget.position = AimTarget.transform.position;
        EffectTarget.rotation = AimTarget.transform.rotation;
    }
}
