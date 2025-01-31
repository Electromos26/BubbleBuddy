using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Enemy;
using Events;
using Player;

namespace Managers
{
    [Serializable]
    public class EnemyType
    {
        public EnemyBase prefab;
        [Range(0, 100)] public float spawnWeight;
        public int minWaveToAppear = 1;
    }

    public class EnemyManager : Singleton<EnemyManager>
    {
        public GameEvent Event;
        [Header("References")]
        [SerializeField] public PlayerController Player { get; private set; }
        
        [Header("Enemy Types")]
        [SerializeField] private List<EnemyType> enemyTypes = new List<EnemyType>();

        
        [Header("Spawn Points")]
        [SerializeField] private List<Transform> spawnPoints;

        private int _enemyKillCount;
        private int _enemySpawnedCount;
        
        private void OnEnable()
        {
            Event.OnEnemyDied += RemoveEnemy;
            Event.OnWaveStart += RecalculateSpawned;
        }
        
        private void OnDisable()
        {
            Event.OnEnemyDied -= RemoveEnemy;
            Event.OnWaveStart -= RecalculateSpawned;
        }
        
        private void RemoveEnemy(EnemyBase enemy)
        {
            _enemyKillCount++;
            if (_enemyKillCount >= WaveManager.Instance.CurrentEnemyPerWave)
            {
                Event.OnWaveEnd?.Invoke();
            }
        }

    private void RecalculateSpawned()
    {
        _enemyKillCount = 0;
        _enemySpawnedCount = 0;
    }

        public void SpawnEnemy()
        {
            var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
            var enemyType = GetRandomEnemyType();
            var enemy =  Instantiate(enemyType.prefab, spawnPoint.position, Quaternion.identity);
            _enemySpawnedCount++;
        }
        
        
        private EnemyType GetRandomEnemyType()
        {
            var currentWave = WaveManager.Instance.CurrentWave;
            var availableEnemies = enemyTypes.Where(e => e.minWaveToAppear <= currentWave).ToList();
            var totalWeight = availableEnemies.Sum(e => e.spawnWeight);
            var randomValue = UnityEngine.Random.Range(0, totalWeight);
            var weightSum = 0f;
            foreach (var enemy in availableEnemies)
            {
                weightSum += enemy.spawnWeight;
                if (randomValue <= weightSum)
                {
                    return enemy;
                }
            }
            
            return enemyTypes[0];
        }

        public void SetPlayer(PlayerController player)
        {
            Player = player;
        }
    }
}