using System.Collections.Generic;

namespace TheWasteland.Gameplay
{
    [System.Serializable]
    public class LevelSystemData
    {
        public float xpModifier;
        public int startingLevel;
        public string[] buffsPaths;
        [System.NonSerialized] public List<BuffSO> startingBuffs;


        public LevelSystemData()
        {
            xpModifier = 1;
            startingLevel = 0;
            buffsPaths = null;
            startingBuffs = new List<BuffSO>();
        }
        public void GetBuffsFromPaths()
        {
            if (buffsPaths == null) return;
            
            startingBuffs = new List<BuffSO>();
            System.Type type = typeof(BuffSO);
            for (int i = 0; i < buffsPaths.Length; i++)
                startingBuffs.Add(UnityEngine.Resources.Load(buffsPaths[i]) as BuffSO);
        }
        public void Add(LevelSystemData extra)
        {
            xpModifier += extra.xpModifier;
            startingLevel += extra.startingLevel;

            if (extra.startingBuffs != null)
            {
                if(startingBuffs == null)
                    startingBuffs = new List<BuffSO>();
                
                startingBuffs.AddRange(extra.startingBuffs);
                SetPaths();
            }
        }
        public void Multiply(LevelSystemData extra)
        {
            if(extra.xpModifier > 0) xpModifier *= extra.xpModifier;
            if(extra.startingLevel > 0) startingLevel *= extra.startingLevel;
            
            if (extra.startingBuffs != null)
            {
                if(startingBuffs == null)
                    startingBuffs = new List<BuffSO>();
                
                startingBuffs.AddRange(extra.startingBuffs);
                SetPaths();
            }
        }
        void SetPaths()
        {
            buffsPaths = new string[startingBuffs.Count];
            for (int i = 0; i < startingBuffs.Count; i++)
            {
                buffsPaths[i] = startingBuffs[i].path;
            }
        }
    }
}