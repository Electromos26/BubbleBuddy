using Player;
using UnityEngine;
using Managers;

namespace Enemy
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private float attackRange;

        public PlayerController Player { get; private set; }
        public bool PlayerInRange { get; set; }

        private Transform _playerTransform;

        private void Awake()
        {
            if (EnemyManager.Instance == null)
            {
                Debug.LogError("EnemyManager is not in Scene or missing");
            }

            Player = EnemyManager.Instance.Player;
            _playerTransform = EnemyManager.Instance.Player.transform;
        }

        public Vector3 GetPlayerDirection()
        {
            return (_playerTransform.position - transform.position).normalized;
        }

        public bool CanAttack()
        {
            return Vector3.Distance(transform.position, _playerTransform.position) < attackRange;
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