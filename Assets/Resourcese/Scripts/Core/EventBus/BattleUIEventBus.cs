using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BattleUIEventType
{
    PlayerHpSet,
    PlayerDie,
    PlayerAmmoSet,
}

public class BattleUIEventBus
{
    static Dictionary<BattleUIEventType, UnityEvent<Player>> events = new Dictionary<BattleUIEventType, UnityEvent<Player>>();

    public static void Subscribe(BattleUIEventType type, UnityAction<Player> listener)
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

    public static void Unsubscribe(BattleUIEventType type, UnityAction<Player> listener)
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(BattleUIEventType type, Player player)
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent?.Invoke(player);
        }
    }
}
