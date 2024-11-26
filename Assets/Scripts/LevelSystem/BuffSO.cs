using UnityEngine;

namespace TheWasteland.Gameplay
{
    [CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buff", order = 0)]
    [System.Serializable]
    public class BuffSO : ScriptableObject
    {
        public enum BuffType { New, Add, Multiply }
        public string name;
        public StatsSO targetStats;
        public StatsSO buff;
        public BuffType type;
        public string path;

        void OnValidate()
        {
            if (targetStats == null) return;
            if (buff == null) return;

            if (buff.GetType() != targetStats.GetType())
                buff = null;

#if UNITY_EDITOR
            path = UnityEditor.AssetDatabase.GetAssetPath(this);
            path = path.Remove(0, "Assets/Resources/".Length);
            path = path.Remove(path.LastIndexOf(".asset"), ".asset".Length);
#endif
        }
    }
}