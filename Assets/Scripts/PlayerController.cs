using System;
using UnityEngine;

namespace TheWasteland.Gameplay.Player 
{ 
    public class PlayerController : MonoBehaviour, IHittable
    {
        [Header("Set Values")]
        [SerializeField] float speed;
        //[Header("Runtime Values")]

        //Unity Events
        void Update()
        {
        }

        //Methods
        public void GetHitted(float dmg)
        {
            
        }
    }
}
