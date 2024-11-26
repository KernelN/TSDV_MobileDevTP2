using UnityEngine;
using Universal.Singletons;

namespace TheWasteland.Shop
{
    public class ShopManager : MonoBehaviourSingletonInScene<ShopManager>, EventManager.IEventListener
    {
        //[Header("Set Values")]
        [SerializeField] TMPro.TextMeshProUGUI coinsText;
        //[Header("Runtime Values")]
        GameManager gm;

        //Unity Events
        void Start()
        {
            EventManager.EventManager.inst.AddListener("BuyItem", this);

            gm = GameManager.inst;
            coinsText.text = "Current Gears: " + gm.GameData.coins.ToString("#000");
        }
        
        //Methods
        public void OnEventRaised(object[] data)
        {
            if(data.Length <= 1) return;
            int coinsSpent = (int)data[0];
            
            if(coinsSpent > gm.GameData.coins) return;

            gm.GameData.coins -= coinsSpent;
            coinsText.text = "Current Gears: " + gm.GameData.coins.ToString("#000");
            
            if (data[1] is Gameplay.BuffSO)
                gm.GameData.levelSystemData.startingBuffs.Add((Gameplay.BuffSO)data[1]);
            else if (data[1] is Gameplay.LevelSystemData)
                gm.GameData.levelSystemData.Add((Gameplay.LevelSystemData)data[1]);
            
            gm.GameData.SaveData();
        }
    }
}