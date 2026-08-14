using UnityEngine;

public class WeaponInputBinder : MonoBehaviour
{
    private IWeaponInput _input;
    private WeaponController _weapons;

    private void Awake()
    {
        _input = GetComponent<IWeaponInput>();
        _weapons = GetComponent<WeaponController>();
    }

    private void OnEnable()
    {
        if (_input == null || _weapons == null)
        {
            Debug.Log($"IWeaponInput 혹은 WeaponController가 비어있습니다");
            return;
        }
        _input.F_HandFire += _weapons.FHandTryFire;
        _input.B_HandFire += _weapons.BHandTryFire;
        _input.F_ShoulderFire += _weapons.FShoulderTryFire;
        _input.B_ShoulderFire += _weapons.BShoulderTryFire;
    }

    private void OnDisable()
    {
        if (_input == null || _weapons == null)
        {
            Debug.Log($"IWeaponInput 혹은 WeaponController가 비어있습니다");
            return;
        }
        _input.F_HandFire -= _weapons.FHandTryFire;
        _input.B_HandFire -= _weapons.BHandTryFire;
        _input.F_ShoulderFire -= _weapons.FShoulderTryFire;
        _input.B_ShoulderFire -= _weapons.BShoulderTryFire;
    }
}