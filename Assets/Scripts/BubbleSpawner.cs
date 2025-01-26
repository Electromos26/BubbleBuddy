using System;
using UnityEngine;
using Managers;
using Random = UnityEngine.Random;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private float cooldownTime = 1f;
    
    private float lastSpawnTime;

    private void Start()
    {
        BubbleSpawnerManager.Instance.RegisterSpawner(this);
    }

    private void OnDestroy()
    {
        BubbleSpawnerManager.Instance.UnregisterSpawner(this);
    }

    public bool CanSpawn()
    {
        return Time.time - lastSpawnTime >= cooldownTime;
    }

    public GameObject SpawnBubble(GameObject bubblePrefab)
    {
        if (!CanSpawn()) return null;

        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        lastSpawnTime = Time.time;
        return bubble;
    }
    
}