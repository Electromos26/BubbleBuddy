using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;
using Enemy;

namespace Managers
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [field: SerializeField] public PlayerController Player { get; set; }
        
        [SerializeField] private int maxEnemiesSpawned = 4;
        
        public List<Transform> spawnPoints;
        public List<EnemyBase> enemiesAlive;
        
        [SerializeField] private List<EnemyBase> enemyPrefabs;

        
        public void SpawnEnemy()
        {
            
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            enemiesAlive.Remove(enemy);
            SpawnEnemy();
        }
    }
}