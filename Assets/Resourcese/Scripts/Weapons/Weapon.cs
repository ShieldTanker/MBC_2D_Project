using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Target {  get; set; }
    public Vector3 WeaponOffset;

    public WeaponData _data;

    void Update()
    {
        if (Target == null) return;

        Target.transform.position = transform.position;
        Target.transform.rotation = transform.rotation;
    }
}
