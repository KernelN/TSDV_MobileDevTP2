using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class PowerCaster : PowerHolder
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        [Header("DEBUG")]
        bool drawGizmos = true;

        //Unity Events
        void Start()
        {
            //Cast power as soon as it has been setted
            power.Cast(transform);
        }
        void Update()
        {
            power.Update(Time.deltaTime);
        }
        void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            if(effectsInOrder.Length == 0) return;
            
            effectsInOrder[0].DrawGizmos(transform);
        }

        //Methods
    }
}
