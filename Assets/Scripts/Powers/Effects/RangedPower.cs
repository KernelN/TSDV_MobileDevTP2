using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    [System.Serializable]
    public class RangedPower : InstantiatorPower
    {
        RangedData data;
        float tickMoveMag;
        
        //Methods
        public override void Set(PowerData powerData)
        {
            base.Set(powerData);
            data = powerData as RangedData;
        }
        public override void Update(float dt)
        {
            tickMoveMag = data.moveSpeed * dt;
            base.Update(dt);
        }
        internal override void OnProjUpdate(Transform proj, float dt)
        {
            proj.transform.position += proj.transform.forward * tickMoveMag;
        }
    }
}