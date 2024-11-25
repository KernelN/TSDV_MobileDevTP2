using UnityEngine;
using TheWasteland.SceneManaging;
using Universal.Singletons;

namespace TheWasteland
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] Scenes currentScene;

        public System.Action SceneChanged;

        public ConfigData config { get; private set; }
        public Gameplay.GameData GameData { get; private set; }


        //Unity Events
        internal override void Awake()
        {
            base.Awake();
            
            if(inst != this) return;
            
            currentScene = SceneLoader.GetCurrentScene();
            LoadAll();
        }
        internal override void OnDestroy()
        {
            if (inst == this)
                QuitGame();
            
            base.OnDestroy();
        }

        //Methods
        public void SetPause(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }
        public void LoadScene(Scenes sceneToLoad, int level)
        {
            SetPause(false); //reset time in case game was paused

            //Update "currentScene" and load
            currentScene = sceneToLoad;
            SceneLoader.LoadScene(currentScene, level);
            SceneChanged?.Invoke();
        }
        public void QuitGame()
        {
            //config.SaveData();
            SaveAll();
            Application.Quit();
        }
        void SaveAll()
        {
            // config.SaveData();
            GameData.SaveData();
        }
        void LoadAll()
        {
            if (config == null)
                config = new ConfigData();

            //config.LoadData();
            
            if (GameData == null)
                GameData = new Gameplay.GameData();
            
            GameData.LoadData();
        }
        
        //Editor Methods
        public void ClearGData()
        {
            // if(gameplayData != null)
            //     gameplayData.Clean();
            // else
            // {
            //     Gameplay.GameplayData gData = new Gameplay.GameplayData();
            //     gData.Clean();
            // }
        }
    }
}