using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public abstract class PowerDecorator : PowerComponent
    {
        protected PowerComponent powerComponent;
        internal Transform transform;

        //Methods
        public PowerDecorator(PowerComponent component) => powerComponent = component;

        public virtual void Set(PowerData powerData, Transform transform)
        {
            this.transform = transform;
        }
        public virtual void Update(float dt) => powerComponent.Update(dt);
        public virtual void Cast(Transform target) => powerComponent.Cast(target);
        public abstract bool GetStats(StatsSO so, out Stats stats);
    }
}