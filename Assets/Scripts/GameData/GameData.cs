using System.Collections.Generic;
using Universal.FileManaging;

namespace TheWasteland.Gameplay
{
    public class GameData
    {
        public int coins;
        public int lastStageUnlocked;
        public LevelSystemData levelSystemData;

        const string dataPath = "/Game.dat";
        
        //Unity Events

        //Methods
        
        public void Clean()
        {
            FixNulls();
            
            coins = 0;
            lastStageUnlocked = 0;
            levelSystemData = new LevelSystemData();
            
            SaveData();
        }
        public void SaveData()
        {
            string path = UnityEngine.Application.persistentDataPath + dataPath;
            FileManager<GameData>.SaveDataToFile(this, path);
        }
        public void LoadData()
        {
            string path = UnityEngine.Application.persistentDataPath + dataPath;
            GameData data = FileManager<GameData>.LoadDataFromFile(path);
            
            if (data == null) return;
            
            coins = data.coins;
            lastStageUnlocked = data.lastStageUnlocked;
            levelSystemData = data.levelSystemData;
            
            FixNulls();
        }
        void FixNulls()
        {
            if(levelSystemData == null) levelSystemData = new LevelSystemData();
        }
    }
}
