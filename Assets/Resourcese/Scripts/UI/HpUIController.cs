using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

public class HpUIController : MonoBehaviour
{
    public Image HpBar;
    public Text HpText;

    Player _player;

    private void Start()
    {
        ServiceLocator sl = ServiceLocator.ForSceneOfLocal(this);
        _player = sl.Get<Player>();
    }

    private void Update()
    {
        if (_player == null) return;

        HpBar.fillAmount = _player._stat.CurrentHp / _player._stat.MaxHp;
        HpText.text = $"{_player._stat.CurrentHp} / {_player._stat.MaxHp}";
    }
}
