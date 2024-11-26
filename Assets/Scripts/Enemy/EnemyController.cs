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
        float invulnerableTimer;

        public System.Action Died;
        
        internal float SqrAttkRange => data.attackRange * data.attackRange;

        //Unity Events
        protected virtual void Update()
        {
            chaseModule.Update();
            if (invulnerableTimer > 0) invulnerableTimer -= Time.deltaTime;
        }

        //Methods
        public virtual void Set(EnemyDataSO data, Transform target, ChaseModule chaseModule)
        {
            this.data = data;
            this.target = target;
            
            cHealth = data.health;

            this.chaseModule = chaseModule;
        }
        public void GetHitted(float dmg)
        {
            if (invulnerableTimer > 0) return;
            invulnerableTimer = invulnerableTime;
            cHealth -= dmg;
            if (cHealth <= 0)
            {
                Died?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
