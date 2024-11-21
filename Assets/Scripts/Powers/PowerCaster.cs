using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class PowerCaster : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] PowerSetter setter;
        //[Header("Runtime Values")]
        PowerComponent power;
        [Header("DEBUG")]
        bool drawGizmos = true;

        //Unity Events
        void Start()
        {
            //Cast power as soon as it has been setted
            power = setter.AssemblePower();
            power.Cast(transform);
        }
        void Update()
        {
            power.Update(Time.deltaTime);
        }
        void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            setter.DrawGizmos(transform);
        }

        //Methods
    }
}
