using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    /// <summary>
    /// Uses powerComponent as hit effect
    /// Uses Cast as secondary Set (as "First Cast")
    /// </summary>
    public class RadialHitPower : PowerDecorator
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        RadialHitData data;
        Transform t;
        float cooldown;
        bool stopsByHit;
        int hitsToStop;

        //Unity Events

        //Methods
        public RadialHitPower(PowerComponent component) : base(component) { }
        public override void Set(PowerData powerData)
        {
            data = powerData as RadialHitData;
            stopsByHit = data.hitsToStop >= 0;
            hitsToStop = data.hitsToStop;
        }
        public override void Update(float dt)
        {
            base.Update(dt);

            if (cooldown > 0)
            {
                cooldown -= dt;
                return;
            }
            
            Collider[] hits;
            hits = Physics.OverlapSphere(t.position, data.hitRadius, data.targetLayers);

            for (int i = 0; i < hits.Length; i++)
            {
                powerComponent.Cast(hits[i].transform);

                //If it stops by hit, stop after X hits and destroy object
                if (!stopsByHit) continue;
                hitsToStop--;
                if (hitsToStop >= 0) continue;
                Object.Destroy(t.gameObject);
                return;
            }
        }
        public override void Cast(Transform target)
        {
            t = target;
        }
    }
}