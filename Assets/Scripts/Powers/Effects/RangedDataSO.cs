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
        
        public RangedData(RangedDataSO so) : base(so)
        {
            moveSpeed = so.moveSpeed;
        }
    }
}