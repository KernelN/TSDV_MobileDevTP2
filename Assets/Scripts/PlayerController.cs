using System;
using UnityEngine;

namespace TheWasteland.Gameplay.Player 
{ 
    public class PlayerController : MonoBehaviour, IHittable
    {
        [Header("Set Values")]
        [SerializeField] Rigidbody rb;
        [SerializeField] float speed;
        [SerializeField] IKFootSolver[] ikFoots;
        //[Header("Runtime Values")]
        InputManager input;

        //Unity Events
        void Start()
        {
            input = InputManager.inst;
        }
        void Update()
        {
            Vector2 dir = input.Axis;
            float sqr = dir.sqrMagnitude;
            for (int i = 0; i < ikFoots.Length; i++)
                ikFoots[i].SetExtraSpeed(sqr);
            
            dir.Normalize();
            Vector3 newVel = new Vector3(dir.x, 0, dir.y) * speed;
            newVel.y = rb.velocity.y;
            rb.velocity = newVel;
        }

        //Methods
        public void GetHitted(float dmg)
        {
            
        }
    }
}