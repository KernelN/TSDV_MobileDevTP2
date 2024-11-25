using TheWasteland.Gameplay.Player;
using UnityEngine;
using Universal.Singletons;

namespace TheWasteland.Gameplay
{
    public class GameplayManager : MonoBehaviourSingletonInScene<GameplayManager>
    {
        //[Header("Set Values")]
        [SerializeField] PlayerController player;
        [SerializeField] PlayerDataSO playerData; 
        [SerializeField] float timeToCompleteStage = 5*60;
        //[Header("Runtime Values")]
        GameManager gameManager;
        LevelManager levelManager;
        Enemy.EnemyManager enemyManager;
        int coins;
        float timer;

        public System.Action GameOver;
        
        public bool StageComplete => timer >= timeToCompleteStage;
        
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
            gameManager = GameManager.inst;

            levelManager = LevelManager.inst;
            levelManager.Set(player, gameManager.GameData.levelSystemData);
            
            enemyManager = Enemy.EnemyManager.inst;
            enemyManager.EnemyDied += (enemyData) => EarnXp(enemyData.xpValue);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if (StageComplete)
            {
                int minutesPostComplete = (int)((timer - timeToCompleteStage) % 60);
                enemyManager.SetSpawnMultiplier(minutesPostComplete*2);
            }
        }

        //Methods
        public void EarnXp(int xpValue)
        {
            if (levelManager.TryLevelUp(xpValue))
                coins++;
        }
    }
}