using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [CreateAssetMenu(fileName = "RangedData", menuName = "Powers/Effects/Ranged")]
    public class RangedDataSO : PowerDataSO
    {
        public GameObject prefab;
        public LayerMask targetLayers;
        [Min(0)] public float detectRange;
        [Min(0)] public float lifeTime;
        [Min(0)] public float moveSpeed;
        
        public override EffectType GetEffectType() => EffectType.Ranged;
        public override PowerData CreateInstance() => new RangedData(this);
        public override void DrawGizmos(Transform t)
        {
            Gizmos.DrawWireSphere(t.position, detectRange);
        }
    }
    
    public class RangedData : PowerData
    {
        public GameObject prefab { get; private set; }
        public LayerMask targetLayers { get; private set; }
        public float detectRange { get; private set; }
        public float lifeTime { get; private set; }
        public float moveSpeed { get; private set; }
        
        public RangedData(RangedDataSO so) : base(so)
        {
            prefab = so.prefab;
            targetLayers = so.targetLayers;
            detectRange = so.detectRange;
            lifeTime = so.lifeTime;
            moveSpeed = so.moveSpeed;
        }
    }
}