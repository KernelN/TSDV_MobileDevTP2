using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public float health;
        public float speed;
    }
}