using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "RangeEnemyData", menuName = "Enemies/RangeEnemy")]
    public class RangedDataSO : EnemyDataSO
    {
        public GameObject projectile;
        public float projectileSpeed;
        public float projectileLifeTime;
        public float launchOffset;
    }
}
