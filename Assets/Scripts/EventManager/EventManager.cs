using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static readonly Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private static readonly Dictionary<string, Delegate> eventDictionaryParam = new Dictionary<string, Delegate>();

    private void OnDestroy()
    {
        eventDictionary.Clear();
        eventDictionaryParam.Clear();
    }

    // Parameterless Events
    public static void StartListening(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] -= listener;
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    // Parameterized Events
    public static void StartListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                eventDictionaryParam[eventName] = typedEvent + listener;
            }
            else
            {
                Debug.LogError($"EventManager: Event {eventName} exists but with a different parameter type.");
            }
        }
        else
        {
            eventDictionaryParam[eventName] = listener;
        }
    }

    public static void StopListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                eventDictionaryParam[eventName] = typedEvent - listener;
                if (eventDictionaryParam[eventName] == null)
                {
                    eventDictionaryParam.Remove(eventName);
                }
            }
        }
    }

    public static void TriggerEvent<T>(string eventName, T param)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                typedEvent?.Invoke(param);
            }
            else
            {
                Debug.LogError($"EventManager: Event {eventName} exists but with a different parameter type.");
            }
        }
    }
}
