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
        [Header("References")]
        [SerializeField] public PlayerController Player { get; private set; }
        
        /*
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

        }
        */


        public void SetPlayer(PlayerController player)
        {
            Player = player;
        }
    }
}