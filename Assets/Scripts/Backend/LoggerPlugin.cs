using UnityEngine;

namespace TheWasteland.Plugins
{
    public class LoggerPlugin : MonoBehaviour
    {
        //Plugin:
        //Con un boton muestra los logs
        //Con un boton registra los logs
        //Con otro los borra
        
        //[Header("Set Values")]
        [SerializeField] TMPro.TextMeshProUGUI label;
        //[Header("Runtime Values")]

        const string pluginPackName = "com.insaustialejandro.logger";
        const string pluginClassName = pluginPackName + ".LoggerPlugin";

#if UNITY_ANDROID || PLATFORM_ANDROID
        AndroidJavaClass pluginClass;
        AndroidJavaObject pluginInst;
#endif
        
        //Unity Events
        void Awake()
        {
            label.text = "Start";

#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginClass = new AndroidJavaClass(pluginClassName);
            pluginInst = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
            
            AndroidJavaClass unityClass =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = 
                unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            pluginInst.Call("Set", activity);
#endif
            
            Application.logMessageReceived += HandleUnityLog;
        }
        void OnDestroy()
        {
            Application.logMessageReceived -= HandleUnityLog;
        }

        //Methods
        public void RegisterTimeLog()
        {
            Debug.Log("Unity - Send Log: " + Time.time);
        }
        public void RegisterLog(string log)
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("SendLog", log);
#endif
        }
        public void GetLogs()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            if (pluginInst != null)
                label.text = pluginInst.Call<string>("GetLogs");
            else
                label.text = "Plugin not found";
#endif
        }
        public void ReadLogs()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("ReadLogs");
            GetLogs();
#endif
        }
        public void ClearLogs()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("ClearLogs");
#endif
        }
        
        //Event Receivers
        void HandleUnityLog(string logString, string stacktrace, LogType type)
        {
            RegisterLog(logString);
        }
    }
}