using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public abstract class PowerDecorator : PowerComponent
    {
        protected PowerComponent powerComponent;

        //Methods
        public PowerDecorator(PowerComponent component) => powerComponent = component;

        public abstract void Set(PowerData powerData);
        public virtual void Update(float dt) => powerComponent.Update(dt);
        public virtual void Cast(Transform target) => powerComponent.Cast(target);
    }
}