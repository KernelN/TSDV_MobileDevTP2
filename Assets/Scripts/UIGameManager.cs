using UnityEngine;

namespace TheWasteland
{
    public class UIGameManager : MonoBehaviour
    {
        GameManager gameManager;
        
        private void Start()
        {
            gameManager = GameManager.inst;
        }

        public void LoadScene(SceneManaging.SceneGetter sceneGetter)
        {
            gameManager.LoadScene(sceneGetter.scene, sceneGetter.level);
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