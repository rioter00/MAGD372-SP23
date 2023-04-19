using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<Type, object> events = new Dictionary<Type, object>();

    public static void AddListener<T>(Action<T> listener)
    {
        if (!events.ContainsKey(typeof(T)))
        {
            events[typeof(T)] = new GameEvent<T>();
        }

        var gameEvent = (GameEvent<T>) events[typeof(T)];
        gameEvent.AddListener(listener);
    }

    public static void RemoveListener<T>(Action<T> listener)
    {
        if (!events.ContainsKey(typeof(T)))
            return;

        var gameEvent = (GameEvent<T>) events[typeof(T)];
        gameEvent.RemoveListener(listener);
    }

    public static void TriggerEvent<T>(T args)
    {
        var gameEvent = (GameEvent<T>) events[typeof(T)];
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
