using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    [System.Serializable]
    public abstract class EnemyFactory
    {        
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        internal GameObject prefab;
        internal EnemyDataSO data;
        internal Transform target;

        //Unity Events

        //Methods
        public EnemyFactory(GameObject prefab, EnemyDataSO data, Transform target)
        {
            this.prefab = prefab;
            this.data = data;
            this.target = target;
        }
        public abstract EnemyController Create(Vector3 spawnPos);
        internal virtual EnemyController Spawn(Vector3 spawnPos)
        {
            GameObject go = Object.Instantiate(prefab, spawnPos, Quaternion.identity);
                
            ChaseModule chase = new RBChase();
            chase.Set(data.speed, go.transform, target);

            EnemyController controller = go.GetComponent<EnemyController>();
            controller.Set(data, target, chase);
            
            return controller;
        }
    }
}
