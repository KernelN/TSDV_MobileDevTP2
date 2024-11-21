using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class PowerHolder : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] internal PowerDataSO[] effectsInOrder;
        //[Header("Runtime Values")]
        internal PowerComponent power;

        //Unity Events
        internal void Awake()
        {
            //Instantiate last effect
            power = CreateEffect(effectsInOrder[^1]);
            
            for (int i = effectsInOrder.Length-2; i >= 0; i--)
            {
                //Instantiate each effect, using previous as decorator  
                power = CreateEffect(effectsInOrder[i], power);
            }
        }

        //Methods
        
        PowerComponent CreateEffect(PowerDataSO data, PowerComponent wrappee = null)
        {
            if (data == null) return null;

            PowerComponent newPower;
            switch (data.GetEffectType())
            {
                case EffectType.Ranged:
                    newPower = new RangedPower();
                    break;
                case EffectType.RadialHit: 
                    newPower = new RadialHitPower(wrappee);
                    break;
                case EffectType.Dmg:
                    newPower = new DamagePower(wrappee);
                    break;
                default: return null;
            }
            
            newPower.Set(data.CreateInstance());
            return newPower;
        }
    }
}