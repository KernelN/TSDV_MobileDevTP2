using System;
using UnityEngine;

namespace TheWasteland.Gameplay
{
    public class UIGameplayManager : MonoBehaviour
    {
        //[Header("Set Values")]
        [SerializeField] GameObject gameOverScreen;
        //[Header("Runtime Values")]
        GameplayManager manager;

        //Unity Events
        void Start()
        {
            manager = GameplayManager.inst;
            manager.GameOver += () =>
            {
                Time.timeScale = 0;
                gameOverScreen.SetActive(true);
            };
        }

        //Methods
    }
}
