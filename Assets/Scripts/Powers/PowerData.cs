using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public enum EffectType { Ranged, Melee, RadialDmg }
    public abstract class PowerDataSO : ScriptableObject
    {
        public abstract EffectType GetEffectType();
        public abstract PowerData CreateInstance();
    }
    public abstract class PowerData { }
}