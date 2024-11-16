using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public class MeleeEnemy : EnemyController
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        MeleeDataSO mData;
        IHittable targetHittable;
        float atkTimer;
        static readonly int OnMeleeAttack = Animator.StringToHash("OnMeleeAttack");

        //Unity Events
        protected override void Update()
        {
            base.Update();

            if (atkTimer > 0)
            {
                atkTimer -= Time.deltaTime;
                return;
            }
            
            Vector3 disp = target.position - transform.position;
            disp.y = 0;
            float sqrDist = disp.sqrMagnitude;
            if (sqrDist > SqrAttkRange) return;
            
            atkTimer = data.attackCooldown;
            targetHittable.GetHitted(mData.attackDmg);
            animator.SetTrigger(OnMeleeAttack);
        }

        //Methods
        public override void Set(EnemyDataSO data, Transform target, ChaseModule chaseModule)
        {
            base.Set(data, target, chaseModule);
            mData = data as MeleeDataSO;
            
            targetHittable = target.GetComponent<IHittable>();
        }
    }
}
