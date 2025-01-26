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
        [SerializeField] private float baseSpawnInterval = 3f;
        [SerializeField] private int maxBubblesOnField = 2;
        [SerializeField] private float bubbleLifetime = 5f;
        
        [Header("Spawner References")]
        [SerializeField] private List<BubbleSpawner> spawners = new List<BubbleSpawner>();
        
        private float spawnTimer;
        private int currentSpawnerIndex;
        private List<GameObject> activeBubbles = new List<GameObject>();

        private void Start()
        {
            ValidateSpawnChances();
            spawnTimer = baseSpawnInterval;
        }

        private void Update()
        {
            if (spawners.Count == 0 || bubbleTypes.Count == 0) return;
            
            CleanupExpiredBubbles();

            if (activeBubbles.Count >= maxBubblesOnField) return;

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnBubble();
                spawnTimer = baseSpawnInterval;
            }
        }

        private void CleanupExpiredBubbles()
        {
            activeBubbles.RemoveAll(bubble => bubble == null);
        }

        private void ValidateSpawnChances()
        {
            float totalChance = bubbleTypes.Sum(type => type.spawnChance);
            if (totalChance == 0) return;
            
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

        private void SpawnBubble()
        {
            GameObject selectedPrefab = SelectBubblePrefab();
            if (selectedPrefab == null) return;

            int attempts = 0;
            while (attempts < spawners.Count)
            {
                if (spawners[currentSpawnerIndex].CanSpawn())
                {
                    GameObject bubble = spawners[currentSpawnerIndex].SpawnBubble(selectedPrefab);
                    if (bubble != null)
                    {
                        activeBubbles.Add(bubble);
                        Destroy(bubble, bubbleLifetime);
                    }
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