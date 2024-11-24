using UnityEngine;

namespace TheWasteland.Gameplay.Enemy 
{ 
    [CreateAssetMenu(fileName = "MeleeEnemyData", menuName = "Enemies/MeleeEnemy")]
    public class MeleeDataSO : EnemyDataSO
    {
        [Min(0)] public float attackDmg;
    }
}
