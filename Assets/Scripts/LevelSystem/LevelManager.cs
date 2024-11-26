using System.Collections.Generic;
using UnityEngine;
using Universal.Singletons;

namespace TheWasteland.Gameplay.Player
{
    public class LevelManager : MonoBehaviourSingletonInScene<LevelManager>
    {
        [Header("General")]
        [SerializeField] int XpToLevelUp = 10;
        [SerializeField] BuffSO[] rewards;
        List<BuffSO> rewardOptions;
        float playerXp;
        int playerLevels;
        float xpModifier;
        PlayerController player;
        
        [Header("UI")]
        [SerializeField] LevelRewardUI[] rewardScreens;

        public System.Action LeveledUp;
        public System.Action RewardSelected;
        
        //Unity Events
        void Start()
        {
            rewardOptions = new List<BuffSO>(rewards);
            for (int i = 0; i < rewardScreens.Length; i++)
                rewardScreens[i].RewardSelected += ApplyReward;
        }

        //Methods
        public void Set(PlayerController player, LevelSystemData data)
        {
            this.player = player;
            
            if(data == null) return;
            
            xpModifier = data.xpModifier;
            
            if(data.startingBuffs != null)
                for (int i = 0; i < data.startingBuffs.Count; i++)
                    ApplyReward(data.startingBuffs[i]);
            
            for (int i = 0; i < data.startingLevel; i++)
                LevelUpPlayer(false);
        }
        public bool TryLevelUp(int dataXpValue)
        {
            playerXp += dataXpValue * xpModifier;

            //If player has enough xp, level up
            if (playerXp < XpToLevelUp * (playerLevels + 1)) return false;
            
            LevelUpPlayer();
            return true;
        }
        void LevelUpPlayer(bool useXP = true)
        {
            //Reset Xp and increase level
            if (useXP)
            {
                playerXp -= XpToLevelUp * playerLevels;
                playerLevels++;
            }
            
            //Set reward options UI
            rewardOptions.Clear();
            rewardOptions.AddRange(rewards);
            for (int i = 0; i < rewardScreens.Length && rewardOptions.Count > 0; i++)
            {
                rewardScreens[i].gameObject.SetActive(false);

                bool rewardSelected = false;
                do
                {
                    int rewardIndex = Random.Range(0, rewardOptions.Count);
                    BuffSO buff = rewardOptions[rewardIndex];
                    rewardOptions.RemoveAt(rewardIndex);
                    
                    Stats current = player.GetStats(buff.targetStats);

                    //If player doesn't have this stats, check if it's an add
                    if (current == null)
                    {
                        if(buff.buff != null) continue;
                        
                        rewardScreens[i].SetReward(buff);
                        rewardSelected = true;
                    }
                    
                    //If player HAS this stats, check if it's an add
                    else
                    {
                        if(buff.buff == null) continue;
                        
                        rewardScreens[i].SetReward(buff, current);
                        rewardSelected = true;
                    }
                } while (!rewardSelected && rewardOptions.Count > 0);
                
                if(rewardSelected) rewardScreens[i].gameObject.SetActive(true);
            }
            
            GameManager.inst.SetPause(true);
            LeveledUp?.Invoke();
        }
        void ApplyReward(BuffSO buff)
        {
            switch (buff.type)
            {
                case BuffSO.BuffType.New:
                    player.AddPower(buff.targetStats);
                    break;
                case BuffSO.BuffType.Add:
                    player.GetStats(buff.targetStats).Add(buff.buff);
                    break;
                case BuffSO.BuffType.Multiply:
                    player.GetStats(buff.targetStats).Multiply(buff.buff);
                    break;
            }
            
            player.ReSet();
            GameManager.inst.SetPause(false);
            RewardSelected?.Invoke();
        }
    }
}