using UnityEngine;
using UnityEngine.UI;

public class LockonUIController : MonoBehaviour
{
    LockonController lockonCon;

    public Image PredictedAim;
    public Image TrackingAim;

    private void OnEnable()
    {
        PlayerBattleUIEventBus.Subscribe(PlayerBattleUIEventType.PlayerLockonSet, OnLockonSet);
    }

    private void OnDisable()
    {
        PlayerBattleUIEventBus.Unsubscribe(PlayerBattleUIEventType.PlayerLockonSet, OnLockonSet);
    }

    void FixedUpdate()
    {
        if (lockonCon == null) return;
        PredictedAim.rectTransform.position = Camera.main.WorldToScreenPoint(lockonCon.PredictedPosition);
        TrackingAim.rectTransform.position = Camera.main.WorldToScreenPoint(lockonCon.TrackingPosition);
    }

    public void OnLockonSet(Player player)
    {
        lockonCon = player.LockonCon;
    }
}
