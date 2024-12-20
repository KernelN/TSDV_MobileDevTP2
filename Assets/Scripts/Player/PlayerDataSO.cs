using UnityEngine;

namespace TheWasteland.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerDataSO : StatsSO
    {
        public int health = 10;
        public float invulnerableTime = .75f;
        public float moveSpeed = 15f;
    }
    public class PlayerData : Stats
    {
        public int health { get; private set; }
        public float invulnerableTime { get; private set; } 
        public float moveSpeed { get; private set; }
        
        PlayerData() { }
        public PlayerData(PlayerDataSO so)
        {
            health = so.health;
            invulnerableTime = so.invulnerableTime;
            moveSpeed = so.moveSpeed;
        }
        public override Stats Copy()
        {
            PlayerData copy = new PlayerData();
            copy.health = health;
            copy.invulnerableTime = invulnerableTime;
            copy.moveSpeed = moveSpeed;
            return copy;
        }
        public override void Add(StatsSO so)
        {
            PlayerDataSO buffSO = so as PlayerDataSO;
            health += buffSO.health;
            invulnerableTime += buffSO.invulnerableTime;
            moveSpeed += buffSO.moveSpeed;
        }
        public override void Multiply(StatsSO so)
        {
            PlayerDataSO buffSO = so as PlayerDataSO;
            if(buffSO.health > 0) health *= buffSO.health;
            if(buffSO.invulnerableTime > 0) invulnerableTime *= buffSO.invulnerableTime;
            if(buffSO.moveSpeed > 0) moveSpeed *= buffSO.moveSpeed;
        }
        public override string ToString()
        {
            string str = "Health: " + health + "\n";
            str += "Invulnerable Time: " + invulnerableTime.ToString(fFormat) + "\n";
            str += "Move Speed: " + moveSpeed.ToString(fFormat);
            return str;
        }
        public override string ToString(StatsSO other)
        {
            if (!(other is PlayerDataSO)) return null;
            PlayerDataSO buffSO = other as PlayerDataSO;
            string str = "";
            
            string temp = "Health: " + health;
            if(buffSO.health > 0)
                str += "<b>" + temp + "</b>\n";
            else
                str += temp + "\n";
            
            temp = "Invulnerable Time: " + invulnerableTime.ToString(fFormat);
            if(buffSO.invulnerableTime > 0)
                str += "<b>" + temp + "</b>\n";
            else
                str += temp + "\n";
            
            temp = "Move Speed: " + moveSpeed.ToString(fFormat);
            if(buffSO.moveSpeed > 0)
                str += "<b>" + temp + "</b>";
            else
                str += temp;
            
            return str;
        }
    }
}