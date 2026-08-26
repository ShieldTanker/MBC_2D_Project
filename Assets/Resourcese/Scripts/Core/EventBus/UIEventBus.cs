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

public class UIEventBus
{
    static Dictionary<Enum, UnityEvent<Player>> events = new Dictionary<Enum, UnityEvent<Player>>();

    public static void Subscribe<T>(T type, UnityAction<Player> listener) where T : Enum
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

    public static void Unsubscribe<T>(T type, UnityAction<Player> listener) where T : Enum
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish<T>(T type, Player player) where T : Enum
    {
        if(events.TryGetValue(type, out UnityEvent<Player> thisEvent))
        {
            thisEvent?.Invoke(player);
        }
    }
}
