using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [System.Serializable]
    public class RangedPower : PowerComponent
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        RangedData data;
        List<Transform> projectiles;
        List<float> projectilesLife;
        Transform caster;
        float cooldown;
        
        //Unity Events

        //Methods
        public void Set(PowerData powerData)
        {
            data = powerData as RangedData;
            projectiles = new List<Transform>();
            projectilesLife = new List<float>();
        }
        public void Update(float dt)
        {
            float moveMag = data.moveSpeed * dt;
            for (int i = 0; i < projectiles.Count; i++)
            {
                do
                {
                    projectiles[i].Translate(moveMag * projectiles[i].forward, Space.World);
                    projectilesLife[i] += dt;
                    
                    if(projectilesLife[i] >= data.lifeTime)
                    {
                        Object.Destroy(projectiles[i].gameObject);
                        projectiles.RemoveAt(i);
                        projectilesLife.RemoveAt(i);
                    }
                } while (i  < projectiles.Count && projectilesLife[i] >= data.lifeTime);
            }

            if (cooldown > 0)
                cooldown -= dt;
            else 
                Cast(caster);
        }
        public void Cast(Transform caster)
        {
            if(cooldown > 0) return;
            
            this.caster = caster;
            
            Collider target = CheckForTargets(caster.position);
            if(!target) return;
            
            Vector3 dir = target.transform.position - caster.position;
            dir.y = 0;
            dir.Normalize();

            Vector3 pos = caster.position + dir * data.launchOffset;
            
            GameObject go = Object.Instantiate(data.prefab, pos, Quaternion.identity);
            go.transform.forward = dir;
            
            projectiles.Add(go.transform);
            projectilesLife.Add(0);
            
            cooldown = data.castCooldown;
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