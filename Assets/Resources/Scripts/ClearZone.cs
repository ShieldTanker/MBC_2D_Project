using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StageUIEventBus.Publish(StageEventType.ClearStage);
    }
}
