using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, object> events = new Dictionary<string, object>();

    public static void AddListener<T>(string key, Action<T> listener)
    {
        if (!events.ContainsKey(key))
        {
            events[key] = new GameEvent<T>();
        }

        var gameEvent = (GameEvent<T>) events[key];
        gameEvent.AddListener(listener);
    }

    public static void RemoveListener<T>(string key, Action<T> listener)
    {
        if (!events.ContainsKey(key))
            return;

        var gameEvent = (GameEvent<T>) events[key];
        gameEvent.RemoveListener(listener);
    }

    public static void TriggerEvent<T>(string key, T args)
    {
        var gameEvent = (GameEvent<T>) events[key];
        gameEvent.Invoke(args);
    }
}

public class GameEvent<T>
{
    private Action<T> listeners;

    public void AddListener(Action<T> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        listeners -= listener;
    }

    public void Invoke(T args)
    {
        listeners?.Invoke(args);
    }
}
