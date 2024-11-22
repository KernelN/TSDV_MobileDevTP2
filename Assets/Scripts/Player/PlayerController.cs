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
            
            if(dt == 0) return;
            if (invulnerableTimer > 0) invulnerableTimer -= Time.deltaTime;
            
            //Get dir
            Vector2 dir = input.Axis;
            
            //Update IKs
            float mag = dir.magnitude;
            for (int i = 0; i < ikFoots.Length; i++)
                ikFoots[i].SetExtraSpeed(mag);

            //Update dir (if there's any dir input)
            if(mag > 0)
                transform.forward = new Vector3(dir.x, 0, dir.y);
            
            
            //Update vel (stops if there's no dir input)
            float speed = data.moveSpeed * mag;
            Vector3 newVel = speed * transform.forward;
            newVel.y = rb.velocity.y;
            rb.velocity = newVel;
            
            //Update powers
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
            ReSet();
        }
        public void ReSet()
        {
            cHealth = data.health;
        }
        public Stats GetStats(StatsSO so)
        {
            if (so is PlayerDataSO) return data;

            for (int i = 0; i < powers.Count; i++)
            {
                if(powers[i].GetStats(so, out Stats stats))
                    return stats;
            }

            return null;
        }
        public void AddPower(StatsSO buffTargetStats)
        {
            int newPowerIndex = powers.Count;
            powers.Add(powerSetter.AssemblePower((Powers.PowerDataSO)buffTargetStats));
            powers[newPowerIndex].Cast(transform);
        }
    }
}