using System;
using UnityEngine;

namespace TheWasteland.Gameplay.Player 
{ 
    public class PlayerController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] float speed;
        [SerializeField] Transform target;
        [SerializeField] IKPolypedSolver ikController;
        //[Header("Runtime Values")]

        //Unity Events
        void Update()
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            
            Vector3 oldPos = transform.position;
            transform.Translate(dir * (speed * Time.deltaTime), Space.World);
            transform.forward = dir;
        }

        //Methods
    }
}
