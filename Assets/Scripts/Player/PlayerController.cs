using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Player 
{ 
    public class PlayerController : MonoBehaviour, IHittable
    {
        [Header("Set Values")]
        [SerializeField] Rigidbody rb;
        [SerializeField] IKFootSolver[] ikFoots;
        [SerializeField] Powers.PowerSetter powerSetter;
        //[Header("Runtime Values")]
        [Header("DEBUG")]
        [SerializeField] bool drawGizmos;
        List<Powers.PowerComponent> powers;
        InputManager input;
        PlayerData data;
        float cHealth;
        float invulnerableTimer;

        public System.Action Died;

        //Unity Events
        void Start()
        {
            input = InputManager.inst;
            powers = new List<Powers.PowerComponent>();
            powers.Add(powerSetter.AssemblePower());
            powers[0].Cast(transform);
        }
        void Update()
        {
            float dt = Time.deltaTime;
            if (invulnerableTimer > 0) invulnerableTimer -= Time.deltaTime;
            
            Vector2 dir = input.Axis;
            float sqr = dir.sqrMagnitude;
            for (int i = 0; i < ikFoots.Length; i++)
                ikFoots[i].SetExtraSpeed(sqr);

            transform.forward = new Vector3(dir.x, 0, dir.y);
            
            Vector3 newVel = transform.forward * data.moveSpeed;
            newVel.y = rb.velocity.y;
            rb.velocity = newVel;
            
            for (int i = 0; i < powers.Count; i++)
                powers[i].Update(dt);
        }
        void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            powerSetter.DrawGizmos(transform);
        }

        //Methods
        public void GetHitted(float dmg)
        {
            if(invulnerableTimer > 0) return;
            cHealth -= dmg;
            invulnerableTimer = data.invulnerableTime;
            if (cHealth <= 0) Died?.Invoke();
        }
        public void Set(PlayerData playerData)
        {
            data = playerData;
            cHealth = data.health;
        }
    }
}