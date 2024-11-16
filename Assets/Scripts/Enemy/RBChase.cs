using UnityEngine;

namespace TheWasteland.Gameplay.Enemy
{
    public class RBChase : ChaseModule
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        Rigidbody rb;

        //Unity Events

        //Methods
        public override void Set(float speed, Transform transform, Transform target)
        {
            base.Set(speed, transform, target);
            
            rb = transform.GetComponent<Rigidbody>();
        }
        public override void Update()
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            
            Vector3 vel = dir * speed;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
            
            transform.LookAt(target, Vector3.up);
        }
    }
}
