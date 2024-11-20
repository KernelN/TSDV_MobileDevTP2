using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public abstract class PowerDecorator : PowerComponent
    {
        protected PowerComponent powerComponent;
        protected PowerData powerData;

        //Methods
        public PowerDecorator(PowerComponent component) => powerComponent = component;

        public virtual void Set(PowerData data) { powerData = data; }
        public virtual void Update(float dt) => powerComponent.Update(dt);
        public virtual void Cast() => powerComponent.Cast();
    }
}