using UnityEngine;

namespace Universal.Singletons
{
    public class MonoBehaviourSingletonInScene<T> : MonoBehaviour where T : Component
    {
        public static T inst { get; private set; }
        internal virtual void Awake()
        {
            if (inst == null)
            {
                inst = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        internal virtual void OnDestroy()
        {
            if (inst == this)
                inst = null;
        }
    }
}