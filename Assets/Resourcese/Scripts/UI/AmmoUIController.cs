using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

public class AmmoUIController : MonoBehaviour
{
    ServiceLocator _sl;
    Player _player;

    public Text _bHandAmmo;
    public Text _fHandAmmo;
    public Text _bShoulderAmmo;
    public Text _fShoulderAmmo;

    WeaponBase _bHand;
    WeaponBase _fHand;
    WeaponBase _bShoulder;
    WeaponBase _fShoulder;

    WeaponController _weaponCon;

    private void Start()
    {
        _sl = ServiceLocator.ForSceneOfLocal(this);
        _player = _sl.Get<Player>();
        _weaponCon = _player.WeaponController;

        _bHand = _weaponCon.B_HandAnchor.Weapon;
        _fHand = _weaponCon.F_HandAnchor.Weapon;
        _bShoulder = _weaponCon.B_ShoulderAnchor.Weapon;
        _fShoulder = _weaponCon.F_ShoulderAnchor.Weapon;
    }

    private void Update()
    {
        if (_weaponCon == null) return;

        _bHandAmmo.text = $"B Hand \n{_bHand.CurrentCapacity} / {_bHand.AmmoRemaining}";
        _fHandAmmo.text = $"F Hand \n{_fHand.CurrentCapacity} / {_fHand.AmmoRemaining}";
        _bShoulderAmmo.text = $"B Shoulder \n{_bShoulder.CurrentCapacity} / {_bShoulder.AmmoRemaining}";
        _fShoulderAmmo.text = $"F Shoulder \n{_fShoulder.CurrentCapacity} / {_fShoulder.AmmoRemaining}";
    }
}
