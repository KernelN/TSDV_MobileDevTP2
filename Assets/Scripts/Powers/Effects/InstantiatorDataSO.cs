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
            base.Add(statsAdd);
            InstantiatorDataSO buffSO = statsAdd as InstantiatorDataSO;
            if(buffSO.prefab)
                prefab = buffSO.prefab;
            lifeTime += buffSO.lifeTime;
            detectRange += buffSO.detectRange;
            launchOffset += buffSO.launchOffset;
        }
        public override void Multiply(StatsSO statsFactors)
        {
            base.Multiply(statsFactors);
            InstantiatorDataSO buffSO = statsFactors as InstantiatorDataSO;
            if(buffSO.prefab) prefab = buffSO.prefab;
            if(buffSO.lifeTime > 0) lifeTime *= buffSO.lifeTime;
            if(buffSO.detectRange > 0) detectRange *= buffSO.detectRange;
            if(buffSO.launchOffset > 0) launchOffset *= buffSO.launchOffset;
        }
        public override string ToString()
        {
            string str = prefab.name + "\n";
            str += base.ToString() + "\n";
            str += "Life Time: " + lifeTime.ToString(fFormat) + "\n";
            str += "Detect Range: " + detectRange.ToString(fFormat) + "\n";
            str += "Launch Offset: " + launchOffset.ToString(fFormat);
            return str;
        }
        public override string ToString(StatsSO other)
        {
            if (!(other is InstantiatorDataSO)) return null;
            InstantiatorDataSO buffSO = other as InstantiatorDataSO;
            string str = "";
            
            //If buff HAS a prefab, and it's different from current
            string temp = "";
            if (buffSO.prefab)
            {
                temp = prefab.name;
                if (buffSO.prefab != prefab)
                    str += "<b>" + temp + "</b>\n";
                else str += temp + "\n";
            }
            
            str += base.ToString(other) + "\n";
            
            temp = "Life Time: " + lifeTime.ToString(fFormat);
            if(buffSO.lifeTime > 0) str += "<b>" + temp + "</b>\n";
            else str += temp + "\n";
            
            temp = "Detect Range: " + detectRange.ToString(fFormat);
            if(buffSO.detectRange > 0) str += "<b>" + temp + "</b>\n";
            else str += temp + "\n";
            
            temp = "Launch Offset: " + launchOffset.ToString(fFormat);
            if(buffSO.launchOffset > 0) str += "<b>" +temp + "</b>\n";
            else str += temp;
            
            return str;
        }

        internal override void Copy(PowerData copyTo)
        {
            base.Copy(copyTo);
            InstantiatorData copy = copyTo as InstantiatorData;
            copy.lifeTime = lifeTime;
            copy.prefab = prefab;
            copy.targetLayers = targetLayers;
            copy.detectRange = detectRange;
            copy.launchOffset = launchOffset;
        }
    }
}