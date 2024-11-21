using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [CreateAssetMenu(fileName = "RadialHitData", menuName = "Powers/Effects/RadialHitData")]
    public class RadialHitDataSO : PowerDataSO
    {
        [Min(0)] public float hitRadius;
        public LayerMask targetLayers;
        [Tooltip("If -1 won't stop by hit count")]
        [Min(-1)] public int hitsToStop = -1; 
        public override EffectType GetEffectType() => EffectType.RadialHit;
        public override PowerData CreateInstance() => new RadialHitData(this);
        public override void DrawGizmos(Transform t)
        {
            Gizmos.DrawWireSphere(t.position, hitRadius);
        }
    }
    public class RadialHitData : PowerData
    {
        public float hitRadius { get; private set; }
        public LayerMask targetLayers { get; private set; }
        public int hitsToStop { get; private set; }
        
        public RadialHitData(RadialHitDataSO data) : base(data)
        {
            hitRadius = data.hitRadius;
            targetLayers = data.targetLayers;
            hitsToStop = data.hitsToStop;
        }
    }
}