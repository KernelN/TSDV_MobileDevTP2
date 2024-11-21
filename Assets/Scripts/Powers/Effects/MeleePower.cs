using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class MeleePower : InstantiatorPower
    {
        MeleeData data;

        //Methods
        public override void Set(PowerData powerData)
        {
            base.Set(powerData);
            data = powerData as MeleeData;
        }
        public override void Cast(Transform caster)
        {
            int projs = projectiles.Count;
            base.Cast(caster);
            
            if(!data.stickToCaster) return;
            if(projs == projectiles.Count) return;
            
            projectiles[^1].transform.parent = caster;
        }
    }
}