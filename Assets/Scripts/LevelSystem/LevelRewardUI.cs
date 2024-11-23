using UnityEngine;

namespace TheWasteland.Gameplay.Player
{
    public class LevelRewardUI : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI titleText;
        [SerializeField] TMPro.TextMeshProUGUI rewardText;
        BuffSO buff;

        public System.Action<BuffSO> RewardSelected;

        public void SetReward(BuffSO buff, Stats currentStats = null)
        {
            this.buff = buff;

            Stats newStats = null;
            switch (buff.type)
            {
                case BuffSO.BuffType.New:
                    rewardText.text = buff.targetStats.ToString();
                    break;
                case BuffSO.BuffType.Add:
                    newStats = currentStats.Copy();
                    newStats.Add(buff.buff);
                    break;
                case BuffSO.BuffType.Multiply:
                    newStats = currentStats.Copy();
                    newStats.Multiply(buff.buff);
                    break;
            }
            
            if(buff.buff)
                rewardText.text = newStats.ToString(buff.buff);
            titleText.text = buff.name;
        }
        public void GetReward()
        {
            RewardSelected?.Invoke(buff);
        }
    }
}