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
        [SerializeField, Min(1)] int stage = 1;
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
                gameManager.GameData.coins += coins;
                gameManager.GameData.SaveData();
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
            bool wasStageComplete = StageComplete;
            timer += Time.deltaTime;
            if (StageComplete)
            {
                int minutesPostComplete = (int)((timer - timeToCompleteStage) % 60);
                enemyManager.SetSpawnMultiplier(minutesPostComplete*2);

                //If game reached end of stage, add coins, just in case player is too OP
                if (!wasStageComplete)
                {
                    //If player unlocked a new stage Update Stage
                    if(stage > gameManager.GameData.lastStageUnlocked)
                        gameManager.GameData.lastStageUnlocked = stage;
                    
                    //Update Coins & reset them
                    gameManager.GameData.coins += coins;
                    coins = 0;
                    
                    //Save
                    gameManager.GameData.SaveData();
                }
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