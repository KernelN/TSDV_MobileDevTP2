using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [CreateAssetMenu(fileName = "RangedData", menuName = "Powers/Effects/Ranged")]
    public class RangedDataSO : InstantiatorDataSO
    {
        [Min(0)] public float moveSpeed;
        
        public override EffectType GetEffectType() => EffectType.Ranged;
        public override PowerData CreateInstance() => new RangedData(this);
    }
    
    public class RangedData : InstantiatorData
    {
        public float moveSpeed { get; private set; }
        
        RangedData() { }
        public RangedData(RangedDataSO so) : base(so)
        {
            moveSpeed = so.moveSpeed;
        }
        public override Stats Copy()
        {
            RangedData copy = new RangedData();
            copy.lifeTime = lifeTime;
            copy.prefab = prefab;
            copy.targetLayers = targetLayers;
            copy.detectRange = detectRange;
            copy.launchOffset = launchOffset;
            copy.moveSpeed = moveSpeed;
            return copy;
        }

        public override void Add(StatsSO statsAdd)
        {
            base.Add(statsAdd);
            RangedDataSO buffSO = statsAdd as RangedDataSO;
            moveSpeed += buffSO.moveSpeed;
        }
        public override void Multiply(StatsSO statsFactors)
        {
            base.Multiply(statsFactors);
            RangedDataSO buffSO = statsFactors as RangedDataSO;
            if(buffSO.moveSpeed > 0) moveSpeed *= buffSO.moveSpeed;
        }
    }
}