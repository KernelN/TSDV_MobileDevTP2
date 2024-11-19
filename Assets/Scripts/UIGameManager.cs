using UnityEngine;

namespace TheWasteland
{
    public class UIGameManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseScreen;
        GameManager gameManager;
        bool isPaused = false;
        
        private void Start()
        {
            gameManager = GameManager.inst;
        }

        public void LoadScene(SceneManaging.SceneGetter sceneGetter)
        {
            gameManager.LoadScene(sceneGetter.scene, sceneGetter.level);
        }
        public void SetPause()
        {
            isPaused = !isPaused;
            gameManager.SetPause(isPaused);
            pauseScreen.SetActive(isPaused);
        }
        public void QuitGame()
        {
            gameManager.QuitGame();
        }
        public void ClearData()
        {
            gameManager.ClearGData();
        }
    }
}