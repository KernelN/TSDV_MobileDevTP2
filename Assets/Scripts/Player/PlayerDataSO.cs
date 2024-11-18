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
}