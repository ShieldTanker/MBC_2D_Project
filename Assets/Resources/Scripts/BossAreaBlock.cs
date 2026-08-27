using UnityEngine;

public class BossAreaBlock : MonoBehaviour
{
    void Start()
    {
        BossUIEventBus.Subscribe(BossBattleUIEventType.BossDie, Open);
    }

    void Open(Health _)
    {
        Destroy(gameObject);
    }
}
