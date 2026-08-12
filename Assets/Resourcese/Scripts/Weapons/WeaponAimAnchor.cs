using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    public Transform AimTarget;

    public bool UseAim = true;
    public Vector3 BaseRot = new Vector3(0, 0, 0);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(BaseRot);
    }

    void Update()
    {
        if (!UseAim || AimTarget == null) return;

        AimTarget.rotation = transform.rotation;
    }
}
