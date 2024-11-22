using TheWasteland.Gameplay.Powers;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public abstract class InstantiatorDataSO : PowerDataSO
    {
        [Min(0)] public float lifeTime;
        public GameObject prefab;
        public LayerMask targetLayers;
        [Tooltip("If detect range is 0, it will always use forward of caster as dir")]
        [Min(0)] public float detectRange;
        [Min(0)] public float launchOffset;
        
        public override void DrawGizmos(Transform t)
        {
            Gizmos.DrawWireSphere(t.position, detectRange);
            Gizmos.DrawWireSphere(t.position + t.forward * launchOffset, .5f);
        }
    }

    public abstract class InstantiatorData : PowerData
    {
        public float lifeTime { get; internal set; }
        public GameObject prefab { get; internal set; }
        public LayerMask targetLayers { get; internal set; }
        public float detectRange { get; internal set; }
        public float launchOffset { get; internal set; }
        
        internal InstantiatorData() { }
        protected InstantiatorData(InstantiatorDataSO so) : base(so)
        {
            prefab = so.prefab;
            targetLayers = so.targetLayers;
            detectRange = so.detectRange;
            launchOffset = so.launchOffset;
            lifeTime = so.lifeTime;
        }
        public override void Add(StatsSO statsAdd)
        {
            InstantiatorDataSO buffSO = statsAdd as InstantiatorDataSO;
            lifeTime += buffSO.lifeTime;
            detectRange += buffSO.detectRange;
            launchOffset += buffSO.launchOffset;
        }
        public override void Multiply(StatsSO statsFactors)
        {
            InstantiatorDataSO buffSO = statsFactors as InstantiatorDataSO;
            if(buffSO.lifeTime > 0) lifeTime *= buffSO.lifeTime;
            if(buffSO.detectRange > 0) detectRange *= buffSO.detectRange;
            if(buffSO.launchOffset > 0) launchOffset *= buffSO.launchOffset;
        }
    }
}
