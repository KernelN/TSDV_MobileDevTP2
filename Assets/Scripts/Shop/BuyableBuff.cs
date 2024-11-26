using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Shop
{
    public class BuyableBuff : BuyableUI
    {
        //[Header("Set Values")]
        [SerializeField] Gameplay.BuffSO buff;
        //[Header("Runtime Values")]
        List<Gameplay.BuffSO> boughtBuffs;

        //Unity Events
        internal override void Start()
        {
            base.Start();
            
            boughtBuffs = GameManager.inst.GameData.levelSystemData.startingBuffs;
            if (boughtBuffs == null) boughtBuffs = new List<Gameplay.BuffSO>();
            
            SetUI();
            
            data.Add(buff);
        }

        //Methods
        internal override void SetUI()
        {
            Gameplay.Stats newStats = GetStats();
            switch (buff.type)
            {
                case Gameplay.BuffSO.BuffType.New:
                    rewardText.text = buff.targetStats.ToString();
                    break;
                case Gameplay.BuffSO.BuffType.Add:
                    newStats.Add(buff.buff);
                    break;
                case Gameplay.BuffSO.BuffType.Multiply:
                    newStats.Multiply(buff.buff);
                    break;
            }
            
            if(buff.buff)
                rewardText.text = newStats.ToString(buff.buff);
            titleText.text = buff.name;
        }
        Gameplay.Stats GetStats()
        {
            Gameplay.Stats stats;
            if (buff.targetStats is Gameplay.Player.PlayerDataSO)
            {
                stats = new Gameplay.Player.PlayerData((Gameplay.Player.PlayerDataSO)buff.targetStats);
            }
            else if (buff.targetStats is Gameplay.Powers.PowerDataSO)
            {
                Gameplay.Powers.PowerDataSO powerSO = (Gameplay.Powers.PowerDataSO)buff.targetStats;
                stats = powerSO.CreateInstance();
            }
            else return null;
            
            for (int i = 0; i < boughtBuffs.Count; i++)
            {
                if(boughtBuffs[i].targetStats == buff.targetStats)
                    switch (buff.type)
                    {
                        case Gameplay.BuffSO.BuffType.Add:
                            stats.Add(boughtBuffs[i].buff);
                            break;
                        case Gameplay.BuffSO.BuffType.Multiply:
                            stats.Multiply(boughtBuffs[i].buff);
                        break;
                    }
            }

            return stats;
        }
    }
}