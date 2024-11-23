using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [System.Serializable]
    public class PowerSetter
    {
        [SerializeField] PowerDataSO[] effectsInOrder;
        
        public PowerComponent AssemblePower()
        {
            //Instantiate last effect
            PowerComponent power = CreateEffect(effectsInOrder[^1]);
            
            for (int i = effectsInOrder.Length-2; i >= 0; i--)
            {
                //Instantiate each effect, using previous as decorator  
                power = CreateEffect(effectsInOrder[i], power);
            }

            return power;
        }
        public PowerComponent AssemblePower(PowerDataSO newEffect)
        {
            //Instantiate last effect
            PowerComponent power = CreateEffect(newEffect);
            
            // for (int i = effectsInOrder.Length-2; i >= 0; i--)
            // {
            //     //Instantiate each effect, using previous as decorator  
            //     power = CreateEffect(effectsInOrder[i], power);
            // }

            return power;
        }
        PowerComponent CreateEffect(PowerDataSO data, PowerComponent wrappee = null)
        {
            if (data == null) return null;

            PowerComponent newPower;
            switch (data.GetEffectType())
            {
                case EffectType.Ranged:
                    newPower = new RangedPower();
                    break;
                case EffectType.Melee:
                    newPower = new MeleePower();
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
        public void DrawGizmos(Transform t)
        {
            if(effectsInOrder == null) return;
            if(effectsInOrder.Length == 0) return;
            if(effectsInOrder[0] == null) return;
            
            effectsInOrder[0].DrawGizmos(t);
        }
    }
}