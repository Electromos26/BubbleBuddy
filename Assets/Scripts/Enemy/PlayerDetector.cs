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
        public bool PlayerInRange { get; set; }  // Changed to public set

        private Transform _playerTransform;

        private void Awake()
        {
            FindPlayer();
        }

        private void Start()
        {
            if (Player == null)
            {
                FindPlayer();
            }
        }

        private void FindPlayer()
        {
            if (EnemyManager.Instance == null)
            {
                Debug.LogError("EnemyManager is not in Scene or missing");
                return;
            }

            if (EnemyManager.Instance.Player == null)
            {
                var foundPlayer = GameObject.FindAnyObjectByType<PlayerController>();  // Updated to new method
                if (foundPlayer != null)
                {
                    EnemyManager.Instance.SetPlayer(foundPlayer);
                }
                else
                {
                    Debug.LogError("Player not found in scene!");
                    return;
                }
            }

            Player = EnemyManager.Instance.Player;
            _playerTransform = Player.transform;
        }

        public Vector3 GetPlayerDirection()
        {
            if (_playerTransform == null)
            {
                FindPlayer();
                return Vector3.zero;
            }
            return (_playerTransform.position - transform.position).normalized;
        }

        public bool CanAttack()
        {
            if (_playerTransform == null)
            {
                FindPlayer();
                return false;
            }
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}