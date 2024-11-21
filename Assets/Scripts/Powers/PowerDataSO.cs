using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public enum EffectType { Ranged, Melee, RadialHit, Dmg }
    public abstract class PowerDataSO : ScriptableObject
    {      
        [Min(0)] public float castCooldown;

        public abstract EffectType GetEffectType();
        public abstract PowerData CreateInstance();
        public virtual void DrawGizmos(Transform t) {}
    }

    public abstract class PowerData
    {
        public float castCooldown { get; internal set; }

        public PowerData(PowerDataSO data)
        {
            castCooldown = data.castCooldown;
        }
    }
}