using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

public class LockonUIController : MonoBehaviour
{
    Player _player;
    LockonController lockonCon;

    public Image PredictedAim;
    public Image TrackingAim;

    private void Start()
    {
        Cursor.visible = false;

        ServiceLocator sl = ServiceLocator.ForSceneOfLocal(this);
        _player = sl.Get<Player>();
        lockonCon = _player.LockonCon;
    }

    // Update is called once per frame
    void Update()
    {
        PredictedAim.rectTransform.position = Camera.main.WorldToScreenPoint(lockonCon.PredictedPosition);
        TrackingAim.rectTransform.position = Camera.main.WorldToScreenPoint(lockonCon.TrackingPosition);
    }
}
