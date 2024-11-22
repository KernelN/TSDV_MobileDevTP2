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
        
        RadialHitData() {}
        public RadialHitData(RadialHitDataSO data) : base(data)
        {
            hitRadius = data.hitRadius;
            targetLayers = data.targetLayers;
            hitsToStop = data.hitsToStop;
        }
        public override Stats Copy()
        {
            RadialHitData copy = new RadialHitData();
            copy.hitRadius = hitRadius;
            copy.targetLayers = targetLayers;
            copy.hitsToStop = hitsToStop;
            return copy;
        }
        public override void Add(StatsSO statsAdd)
        {
            RadialHitDataSO buffSO = statsAdd as RadialHitDataSO;
            hitRadius += buffSO.hitRadius;
            hitsToStop += buffSO.hitsToStop;
        }
        public override void Multiply(StatsSO statsFactors)
        {
            RadialHitDataSO buffSO = statsFactors as RadialHitDataSO;
            if(buffSO.hitRadius > 0) hitRadius *= buffSO.hitRadius;
            if(buffSO.hitsToStop > 0) hitsToStop *= buffSO.hitsToStop;
        }
    }
}