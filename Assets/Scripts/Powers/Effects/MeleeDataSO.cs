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
        
        MeleeData() { }
        public MeleeData(MeleeDataSO data) : base(data)
        {
            stickToCaster = data.stickToCaster;
        }
        public override Stats Copy()
        {
            MeleeData copy = new MeleeData();
            copy.stickToCaster = stickToCaster;
            return copy;
        }
        public override void Add(StatsSO statsAdd)
        {
            base.Add(statsAdd);
            MeleeDataSO buffSO = statsAdd as MeleeDataSO;
            stickToCaster = buffSO.stickToCaster;
        }
        public override void Multiply(StatsSO statsFactors)
        {
            base.Multiply(statsFactors);
            MeleeDataSO buffSO = statsFactors as MeleeDataSO;
            stickToCaster = buffSO.stickToCaster;
        }
        public override string ToString()
        {
            string str = base.ToString() + "\n";
            str += "stickToCaster: " + stickToCaster;
            return str;
        }
        public override string ToString(StatsSO other)
        {
            if (!(other is MeleeDataSO)) return null;
            
            MeleeDataSO buffSO = other as MeleeDataSO;
            string str = base.ToString(other) + "\n";
            
            if(buffSO.stickToCaster != stickToCaster)
                str += "<b>stickToCaster: " + stickToCaster + "</b>";
            else
                str += "stickToCaster: " + stickToCaster;
            
            return str;
        }
    }
}
