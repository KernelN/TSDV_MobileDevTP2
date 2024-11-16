using UnityEngine;

namespace TheWasteland.Gameplay.Enemy 
{ 
    public class MeleeDataSO : EnemyDataSO
    {
        [Min(0)] public float attackDmg;
    }
}
