using UnityEngine;
using Player.States;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
        [SerializeField] private Transform pointerArrow;
        [SerializeField] private Transform bubbleSpawnPoint;

        public PlayerMoveState MoveState;
        public PlayerAttackState AttackState;
        public PlayerIdleState IdleState;

        //public PlayerDashState dashState;
        //public PlayerHurtState hurtState;
        //public PlayerDeathState deathState;


        private PlayerBaseState CurrentState { get; set; }
        public Rigidbody2D Rb { get; private set; }
        public InputManager InputManager { get; private set; }
        public CountdownTimer AttackTimer { get; private set; }

        private Camera mainCamera;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            InputManager = GetComponent<InputManager>();
            mainCamera = Camera.main;
            AttackTimer = new CountdownTimer(PlayerStats.AttackCooldown);
            AttackTimer.Start();

            InitStates();
            ChangeState(IdleState);
        }

        private void Start()
        {
        }

        private void Update()
        {
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
            var mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 0f));
            Vector2 direction = mousePosition - transform.position;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointerArrow.rotation = Quaternion.Euler(0f, 0f, angle);
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
            ChangeState(AttackState);
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