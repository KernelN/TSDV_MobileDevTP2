using UnityEngine;

namespace TheWasteland.Shop
{
    public class BuyableLevelSystem : BuyableUI
    {
        enum RewardType { ExtraXP, StartingLevel }

        //[Header("Set Values")]
        [SerializeField] RewardType type;
        [SerializeField, Min(1)] float modifier;
        //[Header("Runtime Values")]
        Gameplay.LevelSystemData lvlData;
        Gameplay.LevelSystemData rewardData;

        //Unity Events
        internal override void Start()
        {
            base.Start();
            
            lvlData = GameManager.inst.GameData.levelSystemData;

            rewardData = new Gameplay.LevelSystemData();
            switch (type)
            {
                case RewardType.ExtraXP:
                    rewardData.xpModifier = modifier;
                break;
                case RewardType.StartingLevel:
                    rewardData.xpModifier = 0;
                    rewardData.startingLevel = (int)modifier;
                break;
            }
            
            data.Add(rewardData);
            
            SetUI();
        }

        //Methods
        internal override void SetUI()
        {
            switch (type)
            {
                case RewardType.ExtraXP:
                    titleText.text = "XP earned multiplier";
                    rewardText.text = "XP x " + (lvlData.xpModifier + modifier).ToString("#0.#");
                    break;
                case RewardType.StartingLevel:
                    titleText.text = "Starting level";
                    rewardText.text = "Extra Levels: " + (lvlData.startingLevel + modifier).ToString("N0");
                    break;
            }
        }
    }
}
