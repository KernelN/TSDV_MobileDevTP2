using System.Collections.Generic;
using Universal.Singletons;

namespace TheWasteland.EventManager
{
    public class EventManager : MonoBehaviourSingletonInScene<EventManager>
    {
        Dictionary<string, List<IEventListener>> listeners;
        
        internal override void Awake()
        {
            base.Awake();
            
            if(inst != this) return;
            
            listeners = new Dictionary<string, List<IEventListener>>();
        }
        
        public void AddListener(string eventName, IEventListener listener)
        {
            if (!listeners.ContainsKey(eventName))
                listeners.Add(eventName, new List<IEventListener>());
            listeners[eventName].Add(listener);
        }
        public void RemoveListener(string eventName, IEventListener listener)
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
