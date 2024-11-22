using UnityEngine;

namespace TheWasteland.Gameplay
{
    [CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buff", order = 0)]
    public class BuffSO : ScriptableObject
    {
        public enum BuffType { New, Add, Multiply }
        public string name;
        public StatsSO targetStats;
        public StatsSO buff;
        public BuffType type;

        void OnValidate()
        {
            if (targetStats == null) return;
            if (buff == null) return;

            if (buff.GetType() != targetStats.GetType())
                buff = null;
        }
    }
}
