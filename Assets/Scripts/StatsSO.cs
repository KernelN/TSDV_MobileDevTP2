using UnityEngine;

namespace TheWasteland.Gameplay
{
    public abstract class StatsSO : ScriptableObject { }
    public abstract class Stats
    {
        public StatsSO ogSO { get; private set; }
        internal const string fFormat = "#0.##";
        
        public Stats() {} 
        public Stats(StatsSO ogSO) { this.ogSO = ogSO; }
        public abstract Stats Copy();
        public abstract void Add(StatsSO statsAdd);
        public abstract void Multiply(StatsSO statsFactors);
        public virtual string ToString(StatsSO other) => ToString();
        public bool TryApplyBuff(BuffSO buff)
        {
            if(buff.targetStats != ogSO) return false;
            
            switch (buff.type)
            {
                case BuffSO.BuffType.Add:
                    Add(buff.buff);
                    break;
                case BuffSO.BuffType.Multiply:
                    Multiply(buff.buff);
                    break;
            }
            return true;
        }
    }
}
