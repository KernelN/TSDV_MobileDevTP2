using System;
using UnityEngine;

namespace TheWasteland.Gameplay
{
    public class UIInputManager : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] GameObject calibrateButton;
        //[Header("Runtime Values")]
        InputManager manager;

        //Unity Events
        void Start()
        {
            manager = InputManager.inst;
            
            if(manager.inputType == InputManager.InputType.VStick)
                calibrateButton.SetActive(false);
        }

        //Methods
        public void RecalibrateSensors() => manager.RecalibrateSensors(); 
    }
}
