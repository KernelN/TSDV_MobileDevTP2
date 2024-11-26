using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.MainMenu
{
    public class UIStages : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] List<GameObject> stageButtons;
        //[Header("Runtime Values")]
        GameManager gm;

        //Unity Events
        void Start()
        {
            gm = GameManager.inst;
            for (int i = 0; i < stageButtons.Count; i++)
            {
                stageButtons[i].SetActive(gm.GameData.lastStageUnlocked >= i);
            }
        }
        void OnValidate()
        {
            //sort stage buttons list by gameObject name
            if (stageButtons != null && stageButtons.Count > 1)
                stageButtons.Sort((a, b) => a.name.CompareTo(b.name));
        }

        //Methods
    }
}
