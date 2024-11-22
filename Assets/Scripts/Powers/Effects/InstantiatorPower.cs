using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public abstract class InstantiatorPower : PowerComponent
    {
        internal List<Transform> projectiles;
        internal List<float> projectilesLife;
        internal Transform caster;
        internal float cooldown;
        InstantiatorData data;

        public virtual void Set(PowerData powerData)
        {
            data = powerData as InstantiatorData;
            projectiles = new List<Transform>();
            projectilesLife = new List<float>();
        }
        public virtual void Update(float dt)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                do
                {
                    OnProjUpdate(projectiles[i], dt);
                    projectilesLife[i] += dt;

                    if (projectilesLife[i] >= data.lifeTime)
                    {
                        Object.Destroy(projectiles[i].gameObject);
                        projectiles.RemoveAt(i);
                        projectilesLife.RemoveAt(i);
                    }
                } while (i < projectiles.Count && projectilesLife[i] >= data.lifeTime);
            }
            
            if (cooldown > 0)
                cooldown -= dt;
            else 
                Cast(caster);
        }
        internal virtual void OnProjUpdate(Transform proj, float dt) {}
        public virtual void Cast(Transform caster)
        {
            if(cooldown > 0) return;
            
            this.caster = caster;
            
            Vector3 dir;

            if (data.detectRange > 0)
            {
                Collider target = CheckForTargets(caster.position);
                if (!target) return;

                dir = target.transform.position - caster.position;
                dir.y = 0;
                dir.Normalize();
            }
            else
                dir = caster.forward;

            Vector3 pos = caster.position + dir * data.launchOffset;
            
            GameObject go = Object.Instantiate(data.prefab, pos, Quaternion.identity);
            go.transform.forward = dir;
            
            projectiles.Add(go.transform);
            projectilesLife.Add(0);
            
            cooldown = data.castCooldown;
        }
        public bool GetStats(StatsSO so, out Stats stats)
        {
            stats = null;
            if(so != data.ogSO) return false;
            stats = data;
            return true;
        }
        Collider CheckForTargets(Vector3 pos)
        {
            Collider[] hits;
            hits = Physics.OverlapSphere(pos, data.detectRange, data.targetLayers);
            
            if(hits == null || hits.Length == 0) return null;
            
            return hits[0];
        }
    }
}
