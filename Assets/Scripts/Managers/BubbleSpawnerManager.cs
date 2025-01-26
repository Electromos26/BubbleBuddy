using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Managers
{
    [System.Serializable]
    public class BubbleType
    {
        public GameObject prefab;
        [Range(0, 100)] public float spawnChance;
    }

    public class BubbleSpawnerManager : Singleton<BubbleSpawnerManager>
    {
        [Header("Spawn Settings")]
        [SerializeField] private List<BubbleType> bubbleTypes = new List<BubbleType>();
        [SerializeField] private float spawnInterval = 3f;
        
        [Header("Spawner References")]
        [SerializeField] private List<BubbleSpawner> spawners = new List<BubbleSpawner>();
        
        private float spawnTimer;
        private int currentSpawnerIndex;

        private void Start()
        {
            ValidateSpawnChances();
            spawnTimer = spawnInterval;
        }

        private void ValidateSpawnChances()
        {
            float totalChance = bubbleTypes.Sum(type => type.spawnChance);
            if (totalChance == 0) return;
            
            // Normalize chances if they exceed 100%
            if (totalChance > 100)
            {
                float multiplier = 100f / totalChance;
                foreach (var type in bubbleTypes)
                {
                    type.spawnChance *= multiplier;
                }
            }
        }

        private GameObject SelectBubblePrefab()
        {
            float roll = Random.Range(0f, 100f);
            float currentTotal = 0;

            foreach (var type in bubbleTypes)
            {
                currentTotal += type.spawnChance;
                if (roll <= currentTotal)
                {
                    return type.prefab;
                }
            }

            return bubbleTypes.FirstOrDefault()?.prefab;
        }

        private void Update()
        {
            if (spawners.Count == 0 || bubbleTypes.Count == 0) return;

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnBubble();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnBubble()
        {
            GameObject selectedPrefab = SelectBubblePrefab();
            if (selectedPrefab == null) return;

            int attempts = 0;
            while (attempts < spawners.Count)
            {
                if (spawners[currentSpawnerIndex].CanSpawn())
                {
                    spawners[currentSpawnerIndex].SpawnBubble(selectedPrefab);
                    break;
                }
                
                currentSpawnerIndex = (currentSpawnerIndex + 1) % spawners.Count;
                attempts++;
            }

            currentSpawnerIndex = (currentSpawnerIndex + 1) % spawners.Count;
        }

        public void RegisterSpawner(BubbleSpawner spawner)
        {
            if (!spawners.Contains(spawner))
            {
                spawners.Add(spawner);
            }
        }

        public void UnregisterSpawner(BubbleSpawner spawner)
        {
            spawners.Remove(spawner);
        }
    }
}