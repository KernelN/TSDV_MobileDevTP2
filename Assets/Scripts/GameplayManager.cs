using TheWasteland.Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Universal.Singletons;

namespace TheWasteland.Gameplay
{
    public class GameplayManager : MonoBehaviourSingletonInScene<GameplayManager>
    {
        //[Header("Set Values")]
        [SerializeField] PlayerController player;
        [SerializeField] PlayerDataSO playerData;
        //[Header("Runtime Values")]

        public System.Action GameOver;
        
        //Unity Events
        void Awake()
        {
            base.Awake();
            
            if(!player)
                player = FindObjectOfType<PlayerController>();
            player.Set(playerData);
            player.Died += () =>
            {
                Time.timeScale = 0;
                GameOver?.Invoke();
            };
        }

        //Methods
    }
}
