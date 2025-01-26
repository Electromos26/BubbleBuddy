using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Enemy;
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
        public event Action<EnemyBase> OnEnemyDied;
        
        [Header("References")]
        [SerializeField] public PlayerController Player { get; private set; }
        [SerializeField] private WaveManager waveManager;
        
        [Header("Enemy Types")]
        [SerializeField] private List<EnemyType> enemyTypes = new List<EnemyType>();
        
        [Header("Spawn Settings")]
        [SerializeField] private float initialSpawnInterval = 4.5f;
        [SerializeField] private float minimumSpawnInterval = 1.5f;
        [SerializeField] private float spawnIntervalDecreaseRate = 30f;
        [SerializeField] private float spawnRadius = 2f;
        
        [Header("Spawn Points")]
        [SerializeField] private List<Transform> spawnPoints;

        private float currentSpawnInterval;
        private float spawnTimer;
        private List<EnemyBase> activeEnemies = new List<EnemyBase>();

        private void Start()
        {
            currentSpawnInterval = initialSpawnInterval;
            ValidateEnemyWeights();
            
            if (waveManager != null)
            {
                waveManager.OnWaveStart += OnWaveStart;
            }
        }

        private void ValidateEnemyWeights()
        {
            float totalWeight = enemyTypes.Sum(type => type.spawnWeight);
            if (totalWeight == 0) return;
            
            if (totalWeight != 100)
            {
                float multiplier = 100f / totalWeight;
                foreach (var type in enemyTypes)
                {
                    type.spawnWeight *= multiplier;
                }
            }
        }

        private void Update()
        {
            if (!waveManager.IsWaveActive()) return;
            
            UpdateDifficulty();
            
            if (activeEnemies.Count >= waveManager.GetMaxEnemiesForCurrentWave()) return;

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = currentSpawnInterval;
            }
        }

        private void UpdateDifficulty()
        {
            // Spawn Interval formula: max(5 - (Time/30), 1.5)
            currentSpawnInterval = Mathf.Max(5 - (waveManager.GameTime / spawnIntervalDecreaseRate), minimumSpawnInterval);
        }

        private EnemyBase SelectEnemyPrefab()
        {
            int currentWave = waveManager.GetCurrentWave();
            var availableEnemies = enemyTypes.Where(e => e.minWaveToAppear <= currentWave).ToList();
            
            if (availableEnemies.Count == 0) return null;

            float roll = UnityEngine.Random.Range(0f, 100f);
            float currentTotal = 0;

            foreach (var enemyType in availableEnemies)
            {
                currentTotal += enemyType.spawnWeight;
                if (roll <= currentTotal)
                {
                    return enemyType.prefab;
                }
            }

            return availableEnemies[0].prefab;
        }

        private void SpawnEnemy()
        {
            if (spawnPoints.Count == 0) return;

            var enemyPrefab = SelectEnemyPrefab();
            if (enemyPrefab == null) return;

            Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);

            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(enemy);
        }

        private void OnWaveStart(int waveNumber)
        {
            foreach (var enemy in activeEnemies.ToList())
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
            activeEnemies.Clear();
            spawnTimer = currentSpawnInterval;
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            activeEnemies.Remove(enemy);
            OnEnemyDied?.Invoke(enemy);
        }

        public void SetPlayer(PlayerController player)
        {
            Player = player;
        }

        private void OnDrawGizmos()
        {
            if (spawnPoints == null) return;
            
            Gizmos.color = Color.red;
            foreach (var point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, spawnRadius);
                }
            }
        }
    }
}