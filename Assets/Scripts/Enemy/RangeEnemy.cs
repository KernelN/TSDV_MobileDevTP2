using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public class RangeEnemy : EnemyController
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        RangedDataSO rData;
        float atkTimer;
        List<Transform> projectiles;
        List<float> projectilesLife;
        
        
        static readonly int OnAttack = Animator.StringToHash("OnRangedAttack");

        //Unity Events
        void Start()
        {
            projectiles = new List<Transform>();
            projectilesLife = new List<float>();
        }
        protected override void Update()
        {
            float dt = Time.deltaTime;
            float projMove = rData.projectileSpeed * dt;
            float lifetime = rData.projectileLifeTime;
            for (int i = 0; i < projectiles.Count; i++)
            {
                do
                {
                    if (projectiles[i] == null)
                    {
                        DestroyProj(i);
                        continue;
                    }
                    projectiles[i].transform.position += projectiles[i].transform.forward * projMove;
                    
                    projectilesLife[i] += dt;

                    if (projectilesLife[i] >= lifetime)
                        DestroyProj(i);
                } while (i < projectiles.Count && projectilesLife[i] >= lifetime);
            }
            
            
            Vector3 disp = target.position - transform.position;
            disp.y = 0;
            float sqrDist = disp.sqrMagnitude;

            //Only move if target is further than attack range
            if (sqrDist > SqrAttkRange)
            {
                base.Update();
                return;
            }
            
            if (atkTimer > 0)
            {
                atkTimer -= dt;
                return;
            }
            
            Cast(disp.normalized);
            animator.SetTrigger(OnAttack);
        }
        void OnDestroy()
        {
            //Clear all projectiles on defeat
            if(projectiles != null)
                while(projectiles.Count > 0) 
                    DestroyProj(0);
        }

        //Methods
        public override void Set(EnemyDataSO data, Transform target, ChaseModule chaseModule)
        {
            base.Set(data, target, chaseModule);
            rData = data as RangedDataSO;
        }
        void Cast(Vector3 dir)
        {
            if(atkTimer > 0) return;
            Vector3 pos = transform.position + dir * rData.launchOffset;
            
            GameObject go = Instantiate(rData.projectile, pos, Quaternion.identity);
            go.transform.forward = dir;
            
            projectiles.Add(go.transform);
            projectilesLife.Add(0);
            
            atkTimer = rData.attackCooldown;
        }
        void DestroyProj(int index)
        {
            if(projectiles[index])
                Destroy(projectiles[index].gameObject);
            projectiles.RemoveAt(index);
            projectilesLife.RemoveAt(index);
        }
    }
}