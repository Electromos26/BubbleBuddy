using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;
using Enemy;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [field: SerializeField] public PlayerController Player { get; set; }
        public List<EnemyBase> EnemiesAlive { get; private set; } = new();

        [SerializeField] private int maxEnemiesSpawned = 4;

        public List<Transform> spawnPoints;

        [SerializeField] private List<EnemyBase> enemyPrefabs;

        private void OnEnable()
        {
            EventManager.Instance.OnEnemyDied += RemoveEnemy;
        }

        public void SpawnEnemy()
        {
            for (int i = 0; i < maxEnemiesSpawned; i++)
            {
                var enemySpawned = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                    spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
                EnemiesAlive.Add(enemySpawned);
            }
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            EnemiesAlive.Remove(enemy);
            SpawnEnemy();
        }
    }
}