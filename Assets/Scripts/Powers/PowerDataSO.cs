using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public enum EffectType { Ranged, Melee, RadialHit, Dmg }
    public abstract class PowerDataSO : StatsSO
    {      
        [Min(0)] public float castCooldown;

        public abstract EffectType GetEffectType();
        public abstract PowerData CreateInstance();
        public virtual void DrawGizmos(Transform t) {}
        public override string ToString()
        {
            return CreateInstance().ToString();
        }
    }

    public abstract class PowerData : Stats
    {
        public float castCooldown { get; internal set; }

        internal PowerData() { }
        public PowerData(PowerDataSO data) : base(data)
        {
            castCooldown = data.castCooldown;
        }

        public override string ToString()
        {
            return "Cast Cooldown: " + castCooldown.ToString(fFormat);
        }

        public override string ToString(StatsSO other)
        {
            if (!(other is PowerDataSO)) return null;
            PowerDataSO buffSO = other as PowerDataSO;
            
            string str = "";

            string temp = "Cast Cooldown: " + castCooldown.ToString(fFormat);
            if(buffSO.castCooldown > 0)
                str += "<b>" + temp + "</b>";
            else
                str += temp;
            
            return str;
        }
    }
}