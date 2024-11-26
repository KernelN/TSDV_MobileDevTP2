using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public abstract class EnemyController : MonoBehaviour, IHittable
    {
        //[Header("Set Values")]
        [SerializeField] internal Animator animator;
        [SerializeField] internal float invulnerableTime = 1;
        //[Header("Runtime Values")]
        ChaseModule chaseModule;
        internal Transform target;
        internal EnemyDataSO data;
        internal float cHealth;
        List<int> hitterHashes;
        List<float> invulnerableTimers;

        public System.Action Died;
        
        internal float SqrAttkRange => data.attackRange * data.attackRange;

        //Unity Events
        protected virtual void Start()
        {
            hitterHashes = new List<int>();
            invulnerableTimers = new List<float>();
        }
        protected virtual void Update()
        {
            chaseModule.Update();

            float dt = Time.deltaTime;
            for (int i = 0; i < invulnerableTimers.Count; i++)
            {
                invulnerableTimers[i] -= dt;
                if (invulnerableTimers[i] <= 0)
                {
                    hitterHashes.RemoveAt(i);
                    invulnerableTimers.RemoveAt(i);
                }
            }
        }

        //Methods
        public virtual void Set(EnemyDataSO data, Transform target, ChaseModule chaseModule)
        {
            this.data = data;
            this.target = target;
            
            cHealth = data.health;

            this.chaseModule = chaseModule;
        }
        public void GetHitted(float dmg, Transform hitter)
        {
            if (hitterHashes.Contains(hitter.GetHashCode())) return;
            
            cHealth -= dmg;
            if (cHealth <= 0)
            {
                Died?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                hitterHashes.Add(hitter.GetHashCode());
                invulnerableTimers.Add(invulnerableTime);
            }
        }
    }
}
