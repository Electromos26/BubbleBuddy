using Player;
using UnityEngine;
using Managers;

namespace Enemy
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D attackCollider;
        [SerializeField] private float offset = 1f;

        public PlayerController Player { get; private set; }
        public bool PlayerInRange { get; set; }

        private Transform _playerTransform;
        private float _attackRadius;

        private void Awake()
        {
            if (EnemyManager.Instance == null)
            {
                Debug.LogError("EnemyManager is not in Scene or missing");
            }

            Player = EnemyManager.Instance.Player;
            _playerTransform = EnemyManager.Instance.Player.transform;
            _attackRadius = attackCollider.radius;
        }

        public Vector3 GetPlayerDirection()
        {
            return (_playerTransform.position - transform.position).normalized;
        }

        public bool CanAttack()
        {
            return Vector3.Distance(transform.position, _playerTransform.position) < _attackRadius + offset;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PlayerInRange = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PlayerInRange = false;
        }
    }
}