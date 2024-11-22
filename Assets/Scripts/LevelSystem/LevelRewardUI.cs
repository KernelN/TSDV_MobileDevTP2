using UnityEngine;

namespace TheWasteland.Gameplay.Player
{
    public class LevelRewardUI : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI titleText;
        [SerializeField] TMPro.TextMeshProUGUI rewardText;
        BuffSO buff;

        public System.Action<BuffSO> RewardSelected;

        public void SetReward(Stats currentStats, BuffSO buff)
        {
            this.buff = buff;

            Stats newStats = null;
            switch (buff.type)
            {
                case BuffSO.BuffType.New:
                    rewardText.text = buff.targetStats.ToString();
                    return;
                case BuffSO.BuffType.Add:
                    newStats = currentStats.Copy();
                    newStats.Add(buff.buff);
                    break;
                case BuffSO.BuffType.Multiply:
                    newStats = currentStats.Copy();
                    newStats.Multiply(buff.buff);
                    break;
            }
            
            rewardText.text = newStats.ToString();
            titleText.text = buff.name;
        }
        public void GetReward()
        {
            RewardSelected?.Invoke(buff);
        }
    }
}