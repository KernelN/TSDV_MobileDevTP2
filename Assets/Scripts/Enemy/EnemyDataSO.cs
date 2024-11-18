using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public abstract class EnemyDataSO : ScriptableObject
    {
        [Min(0)] public int health;
        [Min(0)] public float speed;
        [Min(0)] public float attackCooldown;
        [Min(0)] public float attackRange;
    }
}