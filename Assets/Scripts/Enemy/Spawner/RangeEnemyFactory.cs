using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    [System.Serializable]
    public class RangeEnemyFactory : EnemyFactory
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]

        //Unity Events

        //Methods
        public override EnemyController Create(Vector3 spawnPos)
        {
            return Spawn(spawnPos);
        }

        public RangeEnemyFactory(GameObject prefab, EnemyDataSO data, Transform target)
                                    : base(prefab, data, target)
        {
        }
    }
}