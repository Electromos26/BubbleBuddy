using UnityEngine;
using Player.States;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
        [field: SerializeField] public PlayerBobber Bobber { get; private set; }
        [SerializeField] private Transform pointerArrow;
        [SerializeField] private Transform bubbleSpawnPoint;

        public PlayerMoveState MoveState;
        public PlayerAttackState AttackState;
        public PlayerIdleState IdleState;

        //public PlayerDashState dashState;
        //public PlayerHurtState hurtState;
        //public PlayerDeathState deathState;


        private PlayerBaseState CurrentState { get; set; }
        private PlayerAnimator Animator { get; set; }
        public InputManager InputManager { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public CountdownTimer AttackTimer { get; private set; }

        private Camera _mainCamera;
        private Vector2 _direction;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            InputManager = GetComponent<InputManager>();
            Bobber = GetComponentInChildren<PlayerBobber>();
            Animator = GetComponentInChildren<PlayerAnimator>();

            _mainCamera = Camera.main;
            AttackTimer = new CountdownTimer(PlayerStats.AttackCooldown);
            AttackTimer.Start();

            InitStates();
            ChangeState(IdleState);
        }

        private void Update()
        {
            Animator.HandlePlayerAnimation(InputManager.Movement);
            CurrentState.UpdateState();
            AttackTimer?.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdateState();
            SetPointerArrow(InputManager.MousePos);
        }

        private void SetPointerArrow(Vector2 mouseInput)
        {
            var mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 0f));
            _direction = mousePosition - transform.position;

            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            var targetRotation = Quaternion.Euler(0f, 0f, angle);
            pointerArrow.rotation = Quaternion.Slerp(pointerArrow.rotation, targetRotation,
                Time.fixedDeltaTime * PlayerStats.ArrowSpeed);
        }

        public void SpawnBullet()
        {
            if (!PlayerStats.BubbleBulletPrefab)
            {
                Debug.LogError("PlayerStats.BubbleBulletPrefab is null");
                return;
            }

            var bubble = Instantiate(PlayerStats.BubbleBulletPrefab, bubbleSpawnPoint.position, Quaternion.identity);
            bubble.Init(_direction);
        }

        #region State Machine

        private void InitStates()
        {
            MoveState = new PlayerMoveState(this);
            AttackState = new PlayerAttackState(this);
            IdleState = new PlayerIdleState(this);
        }

        public void HandleAttack()
        {
            CurrentState.HandleAttack();
        }

        public void ChangeState(PlayerBaseState newState)
        {
            if (CurrentState == newState) return;

            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        #endregion
    }
}