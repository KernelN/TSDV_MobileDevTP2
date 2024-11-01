using System;
using UnityEngine;

namespace NAMESPACENAME
{
    public class PluginLoader : MonoBehaviour
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
        void Start()
        {
            label.text = "Start";

#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginClass = new AndroidJavaClass(pluginClassName);
            pluginInst = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
#endif
        }

        //Methods
        public void RegisterTimeLog()
        {
            Debug.Log("Unity - Send Log: " + Time.time);

#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("SendLog", Time.time.ToString());
#endif
        }
        public void RegisterLog(string log)
        {
            Debug.Log("Unity - Send Log: " + log);

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
        public void SaveLogs()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("SaveLogs");
#endif
        }
        public void ClearLogs()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            pluginInst.Call("ClearLogs");
#endif
        }
    }
}
