using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerBattleUIEventType
{
    PlayerHpSet,
    PlayerDie,
    PlayerAmmoSet,
}

public enum BossBattleUIEventType
{
    BossHpSet,
    BossDie,
}

public enum PlayerUIEventType
{
    EscapeInput,
}

public enum StageEventType
{
    ClearStage
}

public class BossUIEventBus
{
    static Dictionary<BossBattleUIEventType, UnityEvent<Health>> events = new Dictionary<BossBattleUIEventType, UnityEvent<Health>>();

    public static void Subscribe(BossBattleUIEventType type, UnityAction<Health> listener)
    {
        if (events.TryGetValue(type, out UnityEvent<Health> thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<Health>();
            thisEvent.AddListener(listener);
            events.Add(type, thisEvent);
        }
    }

    public static void Unsubscribe(BossBattleUIEventType type, UnityAction<Health> listener)
    {
        if (events.TryGetValue(type, out UnityEvent<Health> thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(BossBattleUIEventType type, Health health)
    {
        if (events.TryGetValue(type, out UnityEvent<Health> thisEvent))
        {
            thisEvent?.Invoke(health);
        }
    }
}

public class PlayerBattleUIEventBus
{
    static Dictionary<PlayerBattleUIEventType, UnityEvent<Player>> events = new Dictionary<PlayerBattleUIEventType, UnityEvent<Player>>();

    public static void Subscribe(PlayerBattleUIEventType type, UnityAction<Player> listener)
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<Player>();
            thisEvent.AddListener(listener);
            events.Add(type, thisEvent);
        }
    }

    public static void Unsubscribe(PlayerBattleUIEventType type, UnityAction<Player> listener)
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(PlayerBattleUIEventType type, Player player)
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent?.Invoke(player);
        }
    }
}

public class PlayerUIEventBus
{
    static Dictionary<PlayerUIEventType, UnityEvent> events = new Dictionary<PlayerUIEventType, UnityEvent>();

    public static void Subscribe(PlayerUIEventType type, UnityAction listener)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            events.Add(type, thisEvent);
        }
    }

    public static void Unsubscribe(PlayerUIEventType type, UnityAction listener)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(PlayerUIEventType type)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}

public class StageUIEventBus
{
    static Dictionary<StageEventType, UnityEvent> events = new Dictionary<StageEventType, UnityEvent>();

    public static void Subscribe(StageEventType type, UnityAction listener)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            events.Add(type, thisEvent);
        }
    }

    public static void Unsubscribe(StageEventType type, UnityAction listener)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(StageEventType type)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}