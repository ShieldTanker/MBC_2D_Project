using UnityEngine;
using UnityEngine.UI;

public class StatusUIController : MonoBehaviour
{
    AgentStat _stat;

    [SerializeField] LoadoutData _loadoutData;

    [SerializeField] Text _headPartName;
    [SerializeField] Text _bodyPartName;
    [SerializeField] Text _armsPartName;
    [SerializeField] Text _legsPartName;

    [SerializeField] Text _fHandPartName;
    [SerializeField] Text _bHandPartName;
    [SerializeField] Text _fShoulderPartName;
    [SerializeField] Text _bShoulderPartName;

    [SerializeField] Text _maxHpText;
    [SerializeField] Text _moveSpeedText;
    [SerializeField] Text _boostSpeedText;
    [SerializeField] Text _jumpForceText;

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();

        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateStat();
        SetStatusUI();
    }

    void SetStatusUI()
    {
        _headPartName.text = _loadoutData.HeadPartData != null ? $"Head : {_loadoutData.HeadPartData.PartName}" : "Head : Not Equip";
        _bodyPartName.text = _loadoutData.BodyPartData != null ? $"Body : {_loadoutData.BodyPartData.PartName}" : "Body : Not Equip";
        _armsPartName.text = _loadoutData.ArmsPartData != null ? $"Arms : {_loadoutData.ArmsPartData.PartName}" : "Arms : Not Equip";
        _legsPartName.text = _loadoutData.LegsPartData != null ? $"Legs : {_loadoutData.LegsPartData.PartName}" : "Legs : Not Equip";

        _fHandPartName.text = _loadoutData.F_HandWeaponData != null ? $"F_Hand : {_loadoutData.F_HandWeaponData.WeaponName}" : "F_Hand : Not Equip";
        _bHandPartName.text = _loadoutData.B_HandWeaponData != null ? $"B_Hand : {_loadoutData.B_HandWeaponData.WeaponName}" : "B_Hand : Not Equip";
        _fShoulderPartName.text = _loadoutData.F_ShoulderWeaponData != null ? $"F_Shoulder : {_loadoutData.F_ShoulderWeaponData.WeaponName}" : "F_Shoulder : Not Equip";
        _bShoulderPartName.text = _loadoutData.B_ShoulderWeaponData != null ? $"B_Shoulder : {_loadoutData.B_ShoulderWeaponData.WeaponName}" : "B_Shoulder : Not Equip";

        _maxHpText.text = $"Max Hp : {_stat.MaxHp}";
        _moveSpeedText.text = $"Move Speed : {_stat.MoveSpeed}";
        _boostSpeedText.text = $"Boost Speed : {_stat.BoostSpeed}";
        _jumpForceText.text = $"Jump Force : {_stat.JumpHeight}";
    }

    void UpdateStat()
    {
        PartStatBlock total = new PartStatBlock();

        // 부위별로 재계산
        if (_loadoutData?.HeadPartData != null) total += _loadoutData.HeadPartData.Stats;
        if (_loadoutData?.BodyPartData != null) total += _loadoutData.BodyPartData.Stats;
        if (_loadoutData?.ArmsPartData != null) total += _loadoutData.ArmsPartData.Stats;
        if (_loadoutData?.LegsPartData != null) total += _loadoutData.LegsPartData.Stats;

        _stat.ApplyStatBlock(total);
    }
}
