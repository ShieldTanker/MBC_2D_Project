using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if(player != null)
               StageUIEventBus.Publish(StageEventType.ClearStage);
        }
    }
}
