using System;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class PowerHolder : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] PowerDataSO[] effectsInOrder;
        //[Header("Runtime Values")]
        PowerDecorator power;

        //Unity Events
        void Start()
        {
            //Instantiate last effect
            
            for (int i = effectsInOrder.Length-1; i >= 0; i++)
            {
                //Instantiate each effect, using previous as decorator  
            }
        }

        //Methods
    }
}