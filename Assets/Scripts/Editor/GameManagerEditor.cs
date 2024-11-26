using UnityEditor;
using UnityEngine;

namespace TheWasteland
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor  : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameManager myScript = (GameManager)target;
            if(GUILayout.Button("Clear Gameplay Data"))
            {
                myScript.ClearGData();
            }
        }
    }
}
