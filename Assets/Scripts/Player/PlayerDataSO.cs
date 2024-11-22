using UnityEngine;

namespace TheWasteland.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        public int health = 10;
        public float invulnerableTime = .75f;
        public float moveSpeed = 15f;
    }
    public class PlayerData
    {
        public int health { get; private set; }
        public float invulnerableTime { get; private set; } 
        public float moveSpeed { get; private set; }
        
        public PlayerData(PlayerDataSO so)
        {
            health = so.health;
            invulnerableTime = so.invulnerableTime;
            moveSpeed = so.moveSpeed;
        }
        public PlayerData(PlayerData oldData)
        {
            health = oldData.health;
            invulnerableTime = oldData.invulnerableTime;
            moveSpeed = oldData.moveSpeed;
        }
        public void Add(PlayerDataSO so)
        {
            health += so.health;
            invulnerableTime += so.invulnerableTime;
            moveSpeed += so.moveSpeed;
        }
        public void Multiply(PlayerDataSO so)
        {
            if(so.health > 0) health *= so.health;
            if(so.invulnerableTime > 0) invulnerableTime *= so.invulnerableTime;
            if(so.moveSpeed > 0) moveSpeed *= so.moveSpeed;
        }
    }
}