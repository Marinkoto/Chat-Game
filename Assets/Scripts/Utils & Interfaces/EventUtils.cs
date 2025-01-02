using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventUtils
{
    /// <summary>
    /// Subscribes multiple UnityEvents to their corresponding actions.
    /// </summary>
    /// <param name="eventActionPairs">A dictionary where the key is the UnityEvent and the value is the corresponding action.</param>
    public static void AddListeners(Dictionary<UnityEvent, Action> eventActionPairs)
    {
        foreach (var pair in eventActionPairs)
        {
            pair.Key.AddListener(() => pair.Value.Invoke());
        }
    }
}
