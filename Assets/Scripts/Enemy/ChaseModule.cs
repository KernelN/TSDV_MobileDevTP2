using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public abstract class ChaseModule
    {
        internal Transform transform;
        internal Transform target;
        internal float speed;
        
        public virtual void Set(float speed, Transform transform, Transform target)
        {
            this.speed = speed;
            this.transform = transform;
            this.target = target;
        }
        public abstract void Update();
    }
}
