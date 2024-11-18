using UnityEngine;

namespace TheWasteland.Gameplay.Player 
{ 
    public class PlayerController : MonoBehaviour, IHittable
    {
        [Header("Set Values")]
        [SerializeField] Rigidbody rb;
        [SerializeField] IKFootSolver[] ikFoots;
        //[Header("Runtime Values")]
        InputManager input;
        PlayerDataSO data;
        float cHealth;
        float invulnerableTimer;

        public System.Action Died;

        //Unity Events
        void Start()
        {
            input = InputManager.inst;
        }
        void Update()
        {
            if (invulnerableTimer > 0) invulnerableTimer -= Time.deltaTime;
            
            Vector2 dir = input.Axis;
            float sqr = dir.sqrMagnitude;
            for (int i = 0; i < ikFoots.Length; i++)
                ikFoots[i].SetExtraSpeed(sqr);
            
            dir.Normalize();
            Vector3 newVel = new Vector3(dir.x, 0, dir.y) * data.moveSpeed;
            newVel.y = rb.velocity.y;
            rb.velocity = newVel;
        }

        //Methods
        public void GetHitted(float dmg)
        {
            if(invulnerableTimer > 0) return;
            cHealth -= dmg;
            invulnerableTimer = data.invulnerableTime;
            if (cHealth <= 0) Died?.Invoke();
        }
        public void Set(PlayerDataSO playerData)
        {
            data = playerData;
            cHealth = data.health;
        }
    }
}