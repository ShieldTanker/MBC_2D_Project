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
        PlayerUIEventBus.Subscribe(PlayerBattleUIEventType.PlayerAmmoSet, SetAmmoRemaining);
    }

    private void OnDisable()
    {
        PlayerUIEventBus.Unsubscribe(PlayerBattleUIEventType.PlayerAmmoSet, SetAmmoRemaining);
    }
    
    void SetAmmoRemaining(Player player)
    {
        WeaponBase fHand = player.WeaponController.F_HandAnchor.Weapon;
        WeaponBase bHand = player.WeaponController.B_HandAnchor.Weapon;
        WeaponBase fShoulder = player.WeaponController.F_ShoulderAnchor.Weapon;
        WeaponBase bShoulder = player.WeaponController.B_ShoulderAnchor.Weapon;

        _bHandAmmo.text = $"B Hand \n{bHand.Context.CurrentCapacity} / {bHand.Context.AmmoRemaining}";
        _fHandAmmo.text = $"F Hand \n{fHand.Context.CurrentCapacity} / {fHand.Context.AmmoRemaining}";
        _bShoulderAmmo.text = $"B Shoulder \n{bShoulder.Context.CurrentCapacity} / {bShoulder.Context.AmmoRemaining}";
        _fShoulderAmmo.text = $"F Shoulder \n{fShoulder.Context.CurrentCapacity} / {fShoulder.Context.AmmoRemaining}";
    }
}