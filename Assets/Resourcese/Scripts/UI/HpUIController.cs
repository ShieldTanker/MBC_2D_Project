using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

public class HpUIController : MonoBehaviour
{
    public Image HpBar;
    public Text HpText;

    public float CurrentHp;
    public float MaxHp;
    Player _player;

    private void OnEnable()
    {
        BattleUIEventBus.Subscribe(BattleUIEventType.PlayerHpSet, SetUI);
    }
    private void OnDisable()
    {
        BattleUIEventBus.Unsubscribe(BattleUIEventType.PlayerHpSet, SetUI);
    }

    private void Start()
    {
        //ServiceLocator sl = ServiceLocator.ForSceneOfLocal(this);
        //_player = sl.Get<Player>();
    }

    private void Update()
    {
        if (_player == null) return;
        CurrentHp = _player._stat.CurrentHp;
        MaxHp = _player._stat.MaxHp;
        HpBar.fillAmount = (float)_player._stat.CurrentHp / _player._stat.MaxHp;
        HpText.text = $"{_player._stat.CurrentHp} / {_player._stat.MaxHp}";
    }

    public void SetUI(Player player)
    {
        CurrentHp = player._stat.CurrentHp;
        MaxHp = player._stat.MaxHp;

        HpBar.fillAmount = (float)player._stat.CurrentHp / player._stat.MaxHp;
        HpText.text = $"{player._stat.CurrentHp} / {player._stat.MaxHp}";
    }
}
