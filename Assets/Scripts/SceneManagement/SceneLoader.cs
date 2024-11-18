using UnityEngine.SceneManagement;
using Universal.SceneManaging;

namespace TheWasteland.SceneManaging
{
    public static class SceneLoader
    {
        public static Scenes GetCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name.Contains("Game"))
            {
                return Scenes.Gameplay;
            }
            else if (currentScene.name.Contains("Menu"))
            {
                return Scenes.MainMenu;
            }
            else if (currentScene.name.Contains("Credits"))
            {
                return Scenes.Credits;
            }
            else if (currentScene.name.Contains("Shop"))
            {
                return Scenes.Shop;
            }
            else
            {
                return Scenes.MainMenu;
            }
        }
        public static void LoadScene(Scenes sceneToLoad, int level = -1)
        {
            string sceneName = "ERROR";

            switch (sceneToLoad)
            {
                case Scenes.Proto:
                    sceneName = SceneManager.GetActiveScene().name;
                    break;
                case Scenes.Gameplay:
                    sceneName = LoadLevel(level);
                    break;
                case Scenes.MainMenu:
                    sceneName = "MainMenu";
                    break;
                case Scenes.Shop:
                    sceneName = "Shop";
                    break;
                case Scenes.Credits:
                    sceneName = "Credits";
                    break;
            }

            ASyncSceneLoader.inst.StartLoad(sceneName);
        }
        static string LoadLevel(int level)
        {
            if (level < 0)
            {
                return "ERROR";
            }
            return "Level_" + level;
        }
    }
}