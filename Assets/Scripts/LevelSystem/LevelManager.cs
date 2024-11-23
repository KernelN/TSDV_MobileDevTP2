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
        PlayerController player;
        
        [Header("UI")]
        [SerializeField] GameObject levelUpScreen;
        [SerializeField] LevelRewardUI[] rewardScreens;

        //Unity Events
        void Start()
        {
            rewardOptions = new List<BuffSO>(rewards);
            for (int i = 0; i < rewardScreens.Length; i++)
                rewardScreens[i].RewardSelected += ApplyReward;
        }

        //Methods
        public void Set(PlayerController player)
        {
            this.player = player;
        }
        public bool TryLevelUp(int dataXpValue)
        {
            playerXp += dataXpValue;

            //If player has enough xp, level up
            if (playerXp < XpToLevelUp * (playerLevels + 1)) return false;
            
            LevelUpPlayer();
            return true;
        }
        void LevelUpPlayer()
        {
            //Reset Xp and increase level
            playerXp -= XpToLevelUp * playerLevels;
            playerLevels++;
            
            //Set reward options UI
            rewardOptions.Clear();
            rewardOptions.AddRange(rewards);
            for (int i = 0; i < rewardScreens.Length && rewardOptions.Count > 0; i++)
            {
                rewardScreens[i].gameObject.SetActive(false);

                bool rewardSelected = false;
                do
                {
                    int rewardIndex = Random.Range(0, rewardOptions.Count - 1);
                    BuffSO buff = rewardOptions[rewardIndex];
                    rewardOptions.RemoveAt(rewardIndex);
                    
                    Stats current = player.GetStats(buff.targetStats);

                    //If player doesn't have this stats, check if it's an add
                    if (current == null)
                    {
                        if(buff.buff != null) continue;
                        
                        rewardScreens[i].SetReward(null, buff);
                        rewardSelected = true;
                    }
                    
                    //If player HAS this stats, check if it's an add
                    else
                    {
                        if(buff.buff == null) continue;
                        
                        rewardScreens[i].SetReward(current, buff);
                        rewardSelected = true;
                    }
                } while (!rewardSelected && rewardOptions.Count > 0);
                
                if(rewardSelected) rewardScreens[i].gameObject.SetActive(true);
            }
            
            //Show level up screen
            levelUpScreen.SetActive(true);
            GameManager.inst.SetPause(true);
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
            levelUpScreen.SetActive(false);
            GameManager.inst.SetPause(false);
        }
    }
}