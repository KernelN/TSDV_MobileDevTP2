using TheWasteland.Gameplay.Player;
using UnityEngine;
using Universal.Singletons;

namespace TheWasteland.Gameplay
{
    public class GameplayManager : MonoBehaviourSingletonInScene<GameplayManager>
    {
        //[Header("Set Values")]
        [SerializeField] PlayerController player;
        [SerializeField] PlayerDataSO playerData; //pensar como conseguir poder nuevo
        //[Header("Runtime Values")]
        LevelManager levelManager;
        int coins;

        public System.Action GameOver;
        
        //Unity Events
        void Awake()
        {
            base.Awake();
            
            if(!player)
                player = FindObjectOfType<PlayerController>();
            player.Set(new PlayerData(playerData));
            player.Died += () =>
            {
                Time.timeScale = 0;
                GameOver?.Invoke();
            };
        }
        void Start()
        {
            levelManager = LevelManager.inst;
            levelManager.Set(player);
        }

        //Methods
        public void EarnXp(int xpValue)
        {
            if (levelManager.TryLevelUp(xpValue))
                coins++;
        }
    }
}