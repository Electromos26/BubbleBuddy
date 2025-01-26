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
    private BubbleSpawnerManager manager;

    private void Awake()
    {
        manager = BubbleSpawnerManager.Instance;
    }

    private void OnEnable()
    {
        if (manager != null)
        {
            manager.RegisterSpawner(this);
        }
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.UnregisterSpawner(this);
        }
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.UnregisterSpawner(this);
        }
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

    private void OnDrawGizmos()
    {
        if (!showGizmo) return;
        
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}