using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [CreateAssetMenu(fileName = "MeleeData", menuName = "Powers/Effects/MeleeData", order = 0)]
    public class MeleeDataSO : InstantiatorDataSO
    {
        public bool stickToCaster = false;
        
        public override EffectType GetEffectType() => EffectType.Melee;
        public override PowerData CreateInstance() => new MeleeData(this);
    }

    public class MeleeData : InstantiatorData
    {
        public bool stickToCaster { get; private set; }
        public MeleeData(MeleeDataSO data) : base(data)
        {
            stickToCaster = data.stickToCaster;
        }
    }
}
