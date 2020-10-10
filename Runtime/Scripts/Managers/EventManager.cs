using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Toolkit
{
    public class CustomEvent : UnityEvent<string, object> { }

    public static class EventManager
    {
        private static Dictionary<string, CustomEvent> eventDictionary = new Dictionary<string, CustomEvent>();

        public static void AddListener(string eventName, UnityAction<string, object> listener)
        {
            CustomEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new CustomEvent();
                thisEvent.AddListener(listener);
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void RemoveListener(string eventName, UnityAction<string, object> listener)
        {
            CustomEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void RemoveAllListeners(string eventName)
        {
            CustomEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveAllListeners();
            }
        }

        public static void Invoke(string eventName, object eventParams)
        {
            CustomEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(eventName, eventParams);
            }
        }
    }
}