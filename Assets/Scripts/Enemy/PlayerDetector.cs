using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public bool HasDetected { get; private set; }
    public bool CanAttack { get; private set; }
    private Transform _playerTransform;
    private CircleCollider2D _collider;
    private float _detectionRadius;

    private void Awake()
    {
        if (player != null)
            _playerTransform = player.transform;

        _collider = GetComponent<CircleCollider2D>();
        _detectionRadius = _collider.radius;
    }

    private bool CanSeePlayer()
    {
        return HasDetected = Vector3.Distance(_playerTransform.position, _playerTransform.position) < _detectionRadius;
    }

    private bool IsCloseToAttack()
    {
        return CanAttack = Vector3.Distance(_playerTransform.position, _playerTransform.position) < _detectionRadius;
    }
}