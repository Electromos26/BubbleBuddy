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
        public GameEvent Event;
        [field: SerializeField] public PlayerController Player { get; set; }
        public List<EnemyBase> EnemiesAlive { get; private set; } = new();

        [SerializeField] private int maxEnemiesSpawned = 4;

        public List<Transform> spawnPoints;

        [SerializeField] private List<EnemyBase> enemyPrefabs;
        
        [Header("Spawn Settings")]
        [SerializeField] private float spawnRadius = 2f;
        [SerializeField] private float cooldownTime = 1f;

        [Header("Gizmo Settings")]
        [SerializeField] private Color gizmoColor = Color.yellow;
        [SerializeField] private bool showGizmo = true;
        
        private float enemyPerWave;

        private void OnEnable()
        {
            Event.OnEnemyDied += RemoveEnemy;
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
    

        private float lastSpawnTime;
        
        public bool CanSpawn()
        {
            return maxEnemiesSpawned > EnemiesAlive.Count;
        }

        public void SpawnBubble(GameObject bubblePrefab)
        {
            if (!CanSpawn()) return;

            //enemyPrefabs[Random.Range(0, enemyPrefabs.Count);
            // Generate random point within circle
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

            // Spawn the bubble
            Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
            lastSpawnTime = Time.time;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmo) return;
        
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            EnemiesAlive.Remove(enemy);
            SpawnEnemy();
        }
    }
}