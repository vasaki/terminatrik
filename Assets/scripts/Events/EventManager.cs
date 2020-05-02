using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    static Dictionary<EventNames, GeneralEvent> events = 
        new Dictionary<EventNames, GeneralEvent>();

    public static void AddListener(EventNames eventName, UnityAction<EventParameter> listener)
    {
        if (!events.ContainsKey(eventName)) events.Add(eventName, new GeneralEvent());
        events[eventName].AddListener(listener);
    }

    public static void Invoke(EventNames eventName, EventParameter eventParameter)
    { 
        if (events.ContainsKey(eventName)) events[eventName].Invoke(eventParameter);
    }

    public static void Invoke(EventNames eventName)
    {
        Invoke(eventName, null);
    }
}
