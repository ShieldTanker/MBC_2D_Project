using UnityEngine;

public class WeaponAimAnchor : MonoBehaviour
{
    public Transform HandTarget;
    public Transform EffectTarget;

    public bool UseAim = true;
    public Vector3 BaseRot = new Vector3(0, 0, 0);
    public Vector3 currRot;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(BaseRot);
    }

    void Update()
    {
        currRot = transform.rotation.eulerAngles;
        if (!UseAim || HandTarget == null) return;

        EffectTarget.position = HandTarget.transform.position;
        EffectTarget.rotation = HandTarget.transform.rotation;
    }

    public void SetTarget(Transform target)
    {

    }
}
