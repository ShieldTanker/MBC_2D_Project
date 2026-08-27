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

    public GameObject BossUI;
    public Image BossHpBar;

    private void OnEnable()
    {
        PlayerBattleUIEventBus.Subscribe(PlayerBattleUIEventType.PlayerHpSet, SetUI);
        BossUIEventBus.Subscribe(BossBattleUIEventType.BossHpSet, SetBossUI);
        BossUIEventBus.Subscribe(BossBattleUIEventType.BossDie, SetBossUIDie);
    }

    private void OnDisable()
    {
        PlayerBattleUIEventBus.Unsubscribe(PlayerBattleUIEventType.PlayerHpSet, SetUI);
        BossUIEventBus.Unsubscribe(BossBattleUIEventType.BossHpSet, SetBossUI);
        BossUIEventBus.Unsubscribe(BossBattleUIEventType.BossDie, SetBossUIDie);
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

    public void SetBossUI(Health health)
    {
        if (BossUI == null || BossHpBar == null)
            return;

        if (!BossUI.activeInHierarchy)
            BossUI.SetActive(true);

        BossHpBar.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
    }

    public void SetBossUIDie(Health health)
    {
        if (BossUI == null || BossHpBar == null)
            return;

        if (BossUI.activeInHierarchy)
            BossUI.SetActive(false);
    }
}
