using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [CreateAssetMenu(fileName = "DamageData", menuName = "Powers/Effects/DamageData")]
    public class DamageDataSO : PowerDataSO
    {
        [Min(0)] public float dmg;
        [Tooltip("How many times will hit each target after first hit (dmg over time)")]
        [Min(0)] public int dmgLoops = 0;
        public override EffectType GetEffectType() => EffectType.Dmg;
        public override PowerData CreateInstance() => new DamageData(this);
    }
    public class DamageData : PowerData
    {
        public float dmg { get; private set; }
        public int dmgLoops { get; private set; }
        
        public DamageData(DamageDataSO data) : base(data)
        {
            dmg = data.dmg;
            dmgLoops = data.dmgLoops;
        }
    }
}