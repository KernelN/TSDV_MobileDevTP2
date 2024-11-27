using System.Collections;
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
        [SerializeField] GameObject stageCompleteText;
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
                
                if(manager.StageComplete)
                    stageCompleteText.SetActive(true);
                
                gameOverScreen.SetActive(true);
                inGameScreen.SetActive(false);
            };
            
            Player.LevelManager lvlManager = Player.LevelManager.inst;
            lvlManager.LeveledUp += () =>
            {
                StartCoroutine(UpdateInGameScreen(false));
                levelUpScreen.SetActive(true);
            };
            lvlManager.RewardSelected += () =>
            {
                StartCoroutine(UpdateInGameScreen(true));
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
        IEnumerator UpdateInGameScreen(bool show)
        {
            yield return new WaitForEndOfFrame();
            inGameScreen.SetActive(show);
        }
    }
}