using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

public class AmmoUIController : MonoBehaviour
{
    public Text _bHandAmmo;
    public Text _fHandAmmo;
    public Text _bShoulderAmmo;
    public Text _fShoulderAmmo;

    private void OnEnable()
    {
        BattleUIEventBus.Subscribe(BattleUIEventType.PlayerAmmoSet, SetAmmoRemaining);
    }

    private void OnDisable()
    {
        BattleUIEventBus.Unsubscribe(BattleUIEventType.PlayerAmmoSet, SetAmmoRemaining);
    }
    
    void SetAmmoRemaining(Player player)
    {
        WeaponBase fHand = player.WeaponController.F_HandAnchor.Weapon;
        WeaponBase bHand = player.WeaponController.B_HandAnchor.Weapon;
        WeaponBase fShoulder = player.WeaponController.F_ShoulderAnchor.Weapon;
        WeaponBase bShoulder = player.WeaponController.B_ShoulderAnchor.Weapon;

        _bHandAmmo.text = $"B Hand \n{bHand.CurrentCapacity} / {bHand.AmmoRemaining}";
        _fHandAmmo.text = $"F Hand \n{fHand.CurrentCapacity} / {fHand.AmmoRemaining}";
        _bShoulderAmmo.text = $"B Shoulder \n{bShoulder.CurrentCapacity} / {bShoulder.AmmoRemaining}";
        _fShoulderAmmo.text = $"F Shoulder \n{fShoulder.CurrentCapacity} / {fShoulder.AmmoRemaining}";
    }
}