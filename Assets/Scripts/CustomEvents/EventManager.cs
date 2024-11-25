using System.Collections.Generic;
using UnityEngine;
using Universal.Singletons;

namespace TheWasteland.EventManager
{
    public class EventManager : MonoBehaviourSingletonInScene<EventManager>
    {
        Dictionary<string, List<EventListener>> listeners;
        
        internal override void Awake()
        {
            base.Awake();
            
            if(inst != this) return;
            
            listeners = new Dictionary<string, List<EventListener>>();
        }
        
        public void AddListener(string eventName, EventListener listener)
        {
            if (!listeners.ContainsKey(eventName))
                listeners.Add(eventName, new List<EventListener>());
            listeners[eventName].Add(listener);
        }
        public void RemoveListener(string eventName, EventListener listener)
        {
            if (listeners.ContainsKey(eventName))
                listeners[eventName].Remove(listener);
        }
        public void TriggerEvent(string eventName, object[] data)
        {
            if (listeners.ContainsKey(eventName))
            {
                for (int i = 0; i < listeners[eventName].Count; i++)
                    listeners[eventName][i].OnEventRaised(data);
            }
        }
    }
}
