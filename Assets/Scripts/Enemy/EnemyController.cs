using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        ChaseModule chaseModule;
        Transform target;
        EnemyDataSO data;
        float cHealth;

        public System.Action Died;

        //Unity Events
        protected virtual void Update()
        {
            chaseModule.Update();
        }

        //Methods
        public void Set(EnemyDataSO data, Transform target, ChaseModule chaseModule)
        {
            this.data = data;
            this.target = target;
            
            cHealth = data.health;

            this.chaseModule = chaseModule;
        }
    }
}
