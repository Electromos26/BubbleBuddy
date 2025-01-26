using UnityEngine;
using Managers;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private float cooldownTime = 1f;
    
    [Header("Gizmo Settings")]
    [SerializeField] private Color gizmoColor = Color.yellow;
    [SerializeField] private bool showGizmo = true;

    private float lastSpawnTime;

    private void OnEnable()
    {
        BubbleSpawnerManager.Instance.RegisterSpawner(this);
    }

    private void OnDisable()
    {
        BubbleSpawnerManager.Instance.UnregisterSpawner(this);
    }

    public bool CanSpawn()
    {
        return Time.time - lastSpawnTime >= cooldownTime;
    }

    public void SpawnBubble(GameObject bubblePrefab)
    {
        if (!CanSpawn()) return;

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
}