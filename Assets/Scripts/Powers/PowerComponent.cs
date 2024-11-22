using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public interface PowerComponent
    {
        public void Set(PowerData powerData);
        public void Update(float dt);

        public void Cast(Transform target);
        public bool GetStats(StatsSO so, out Stats stats);
    }
}