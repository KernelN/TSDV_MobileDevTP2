using UnityEngine;

namespace TheWasteland.Gameplay
{
    public interface IHittable
    {
        public void GetHitted(float dmg, Transform hitter);
    }
}