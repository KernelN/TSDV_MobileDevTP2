using System.Collections.Generic;
using UnityEngine;
using Universal.Singletons;
using Random = UnityEngine.Random;

namespace TheWasteland.Gameplay.Enemy
{
    public class EnemyManager : MonoBehaviourSingletonInScene<EnemyManager>
    {
        enum EnemyType { Melee, Range };

        [System.Serializable]
        struct WaveData
        {
            [Min(0)] public float startTime;
            [Min(-1), Tooltip("If -1, wave never stops")] public float stopTime;
            [Min(0)] public float respawnTime;
            [Min(0)] public int count;
            public EnemyType type;
            public GameObject prefab;
            public EnemyDataSO data;
        }
        
        [Header("Set Values")]
        //General
        [SerializeField] Transform player;
        [SerializeField, Min(0)] float spawnMinDist = 10;
        
        //Enemy count
        [Tooltip("This is a hard limit, not overrided even by spawn multiplier")]
        [SerializeField] int maxEnemies = 50;
        int currentEnemies = 0;
        
        //Waves
        [SerializeField] WaveData[] waves;
        int waveIndex = 0;
        List<(EnemyFactory, int)> factories;
        Dictionary<int, List<EnemyController>> waveEnemies;
        Dictionary<int, float> respawnTimers;
        float timer;
        int spawnMultiplier = 1;
        
        [Header("DEBUG")]
        [SerializeField] bool drawGizmos = false;
        [SerializeField] Color gizmosColor = Color.Lerp(Color.red, Color.clear, 0.5f);

        public System.Action<EnemyDataSO> EnemyDied;
        
        //Unity Events
        void Start()
        {
            factories = new List<(EnemyFactory, int)>();
            waveEnemies = new Dictionary<int, List<EnemyController>>();
            respawnTimers = new Dictionary<int, float>();
        }
        void Update()
        {
            float dt = Time.deltaTime;
            timer += dt;
            
            int newIndex = waveIndex;
            
            while (newIndex < waves.Length-1 && timer >= waves[newIndex].startTime)
                newIndex++;
            
            if (newIndex != waveIndex)
            {
                for (int i = waveIndex; i < newIndex; i++)
                {
                    //Get wave factory
                    EnemyFactory factory = null;
                    switch (waves[i].type)
                    {
                        case EnemyType.Melee:
                            factory = new MeleeEnemyFactory(waves[i].prefab, waves[i].data, player);
                            break;
                        case EnemyType.Range:
                            factory = new RangeEnemyFactory(waves[i].prefab, waves[i].data, player);
                            break;
                        default: return;
                    }

                    //Spawn first wave
                    waveEnemies.TryAdd(i, new List<EnemyController>());
                    for (int j = 0; j < waves[i].count && currentEnemies < maxEnemies; j++)
                        SpawnEnemy(factory, i);
                    
                    //Add factory
                    factories.Add((factory, i));
                }
                waveIndex = newIndex;
            }
            
            //Remove old factories & respawn enemies
            for (int i = 0; i < factories.Count; i++)
            {
                //End waves whose time has passed
                while (i < factories.Count && timer >= waves[factories[i].Item2].stopTime)
                {
                    if(waves[factories[i].Item2].stopTime == -1) break;
                    waveEnemies.Remove(factories[i].Item2);
                    respawnTimers.Remove(factories[i].Item2);
                    factories.RemoveAt(i);
                }
                if (i >= factories.Count) break;

                
                //Wait for respawn cooldown
                respawnTimers.TryGetValue(factories[i].Item2, out float respawnTimer);
                if (respawnTimer < waves[factories[i].Item2].respawnTime)
                {
                    respawnTimer += dt;
                    respawnTimers[factories[i].Item2] = respawnTimer;
                    continue;
                }
                
                //Check if there's space for more enemies 
                if(currentEnemies >= maxEnemies) continue;
                
                //Check if there's space in the wave for more enemies
                List<EnemyController> enemies;
                if(!waveEnemies.TryGetValue(factories[i].Item2, out enemies)) continue;
                if (enemies.Count >= waves[factories[i].Item2].count * spawnMultiplier)
                    continue;
                
                //Spawn new enemy
                SpawnEnemy(factories[i].Item1, factories[i].Item2);
            }
        }
        void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            
            Gizmos.color = gizmosColor;
            if(player)
                Gizmos.DrawSphere(player.position, spawnMinDist*2);
        }

        //Methods
        public void SetSpawnMultiplier(int multiplier)
        {
            if(multiplier > 0)
                spawnMultiplier = multiplier;
        }
        Vector3 GetSpawnPos()
        {
            Vector3 dir;
            Vector3 pos;
            bool validPos = false;
            do
            {
                dir = new Vector3(Random.Range(-1f, 1f), 0 , Random.Range(-1f, 1f));
                dir.Normalize();   
                
                pos = player.position + dir * spawnMinDist;
                pos.y += 20;
                
                validPos = Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 50);
                pos.y = hit.point.y + 1;
            } while(!validPos);
            
            return pos;
        }
        void SpawnEnemy(EnemyFactory factory, int waveIndex)
        {
            if(!waveEnemies.ContainsKey(waveIndex)) return;
            
            EnemyController e = factory.Create(GetSpawnPos());
            waveEnemies[waveIndex].Add(e);

            e.Died += () => OnEnemyDied(waveIndex);

            currentEnemies++;
        }
        
        //Event Receivers
        void OnEnemyDied(int waveIndex)
        {
            currentEnemies--;
            if (waveEnemies.TryGetValue(waveIndex, out List<EnemyController> enemies))
            {
                if (enemies.Count == 0) return;
                //gameplayManager.EarnXp(enemies[0].data.xpValue);
                EnemyDied?.Invoke(enemies[0].data);
                enemies.RemoveAt(0);
                waveEnemies[waveIndex] = enemies;
            }
        }
    }
}