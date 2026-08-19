using UnityEngine;

public class WeaponInputBinder : MonoBehaviour
{
    private IWeaponInput _input;
    private WeaponInputController _weapons;

    private void Awake()
    {
        _input = GetComponent<IWeaponInput>();
        _weapons = GetComponent<WeaponInputController>();
    }

    private void OnEnable()
    {
        if ( _weapons == null)
        {
            Debug.Log($"WeaponInputController가 비어있습니다");
            return;
        }
        if (_input == null)
        {
            Debug.Log($"IWeaponInput 가 비어있습니다");
            return;
        }
        _input.F_HandPerformedFire += _weapons.FHandPerformedFire;
        _input.B_HandPerformedFire += _weapons.BHandPerformedFire;
        _input.F_ShoulderPerformedFire += _weapons.FShoulderPerformedFire;
        _input.B_ShoulderPerformedFire += _weapons.BShoulderPerformedFire;

        _input.F_HandCanceledFire += _weapons.FHandCanceledFire;
        _input.B_HandCanceledFire += _weapons.BHandCanceledFire;
        _input.F_ShoulderCanceledFire += _weapons.FShoulderCanceledFire;
        _input.B_ShoulderCanceledFire += _weapons.BShoulderCanceledFire;
    }

    private void OnDisable()
    {
        if (_input == null || _weapons == null)
        {
            Debug.Log($"IWeaponInput 혹은 WeaponController가 비어있습니다");
            return;
        }
        _input.F_HandPerformedFire -= _weapons.FHandPerformedFire;
        _input.B_HandPerformedFire -= _weapons.BHandPerformedFire;
        _input.F_ShoulderPerformedFire -= _weapons.FShoulderPerformedFire;
        _input.B_ShoulderPerformedFire -= _weapons.BShoulderPerformedFire;

        _input.F_HandCanceledFire -= _weapons.FHandCanceledFire;
        _input.B_HandCanceledFire -= _weapons.BHandCanceledFire;
        _input.F_ShoulderCanceledFire -= _weapons.FShoulderCanceledFire;
        _input.B_ShoulderCanceledFire -= _weapons.BShoulderCanceledFire;
    }
}