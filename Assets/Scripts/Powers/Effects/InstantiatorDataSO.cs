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
        public float lifeTime { get; private set; }
        public GameObject prefab { get; private set; }
        public LayerMask targetLayers { get; private set; }
        public float detectRange { get; private set; }
        public float launchOffset { get; private set; }
        
        protected InstantiatorData(InstantiatorDataSO so) : base(so)
        {
            prefab = so.prefab;
            targetLayers = so.targetLayers;
            detectRange = so.detectRange;
            launchOffset = so.launchOffset;
            lifeTime = so.lifeTime;
        }
    }
}
