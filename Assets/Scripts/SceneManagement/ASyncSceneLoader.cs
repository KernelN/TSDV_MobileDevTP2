using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Universal.Singletons;

namespace Universal.SceneManaging
{
    public class ASyncSceneLoader : MonoBehaviourSingleton<ASyncSceneLoader>
    {
        public string sceneLoading { get; private set; }
        [SerializeField] float minLoadSeconds;
        bool loadIsDone;

        //Unity Events

        //Methods
        public void StartLoad(string sceneToLoad, bool useLoadingScene = true)
        {
            sceneLoading = sceneToLoad;
            if(useLoadingScene)
                SceneManager.LoadScene("LoadScene");
            StartCoroutine(LoadAsyncScene());
        }
        public void StartLoad(int sceneToLoad, bool useLoadingScene = true)
        {
            sceneLoading = SceneManager.GetSceneByBuildIndex(sceneToLoad).name;
            if(useLoadingScene)
                SceneManager.LoadScene("LoadScene");
            StartCoroutine(LoadAsyncScene());
        }
        IEnumerator LoadAsyncScene()
        {
            yield return null; //wait 1 tick so LoadScene finishes loading
            
            //The Application loads the Scene in the background as the current Scene runs.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneLoading);
            asyncLoad.allowSceneActivation = false;

            //Set timer
            float timer = 0;

            // Wait until the asynchronous scene fully loads
            do
            {
                timer += Time.unscaledDeltaTime;
                //Debug.Log("T: " + timer + " | P: " + asyncLoad.progress);
                yield return null;
            } while (timer < minLoadSeconds);

            //Debug.Log("Finished loading " + sceneLoading);
            asyncLoad.allowSceneActivation = true;
        }
    }
}