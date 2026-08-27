using UnityEngine;

public class BossAreaBlock : MonoBehaviour
{
    void OnEnable()
    {
        BossUIEventBus.Subscribe(BossBattleUIEventType.BossDie, Open);
    }

    private void OnDisable()
    {
        BossUIEventBus.Unsubscribe(BossBattleUIEventType.BossDie, Open);
    }

    void Open(Health _) => Destroy(gameObject);
}
