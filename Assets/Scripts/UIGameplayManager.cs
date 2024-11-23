using UnityEngine;

namespace TheWasteland.Gameplay
{
    public class UIGameplayManager : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] GameObject inGameScreen;
        [SerializeField] GameObject levelUpScreen;
        [SerializeField] GameObject pauseScreen;
        [SerializeField] GameObject gameOverScreen;
        //[Header("Runtime Values")]
        GameManager gameManager;
        GameplayManager manager;
        bool isPaused = false;

        //Unity Events
        void Start()
        {
            gameManager = GameManager.inst;
            
            manager = GameplayManager.inst;
            manager.GameOver += () =>
            {
                Time.timeScale = 0;
                gameOverScreen.SetActive(true);
                inGameScreen.SetActive(false);
            };
            
            Player.LevelManager lvlManager = Player.LevelManager.inst;
            lvlManager.LeveledUp += () =>
            {
                inGameScreen.SetActive(false);
                levelUpScreen.SetActive(true);
            };
            lvlManager.RewardSelected += () =>
            {
                inGameScreen.SetActive(true);
                levelUpScreen.SetActive(false);
            };
        }

        //Methods
        public void SetPause()
        {
            isPaused = !isPaused;
            gameManager.SetPause(isPaused);
            pauseScreen.SetActive(isPaused);
        }
    }
}