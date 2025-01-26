using System;
using Managers;
using UnityEngine;
using Player.States;
using UI;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public GameEvent Event;
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
        [field: SerializeField] public PlayerBobber Bobber { get; private set; }
        [field: SerializeField] public PlayerShrinker PlayerShrinker { get; private set; }

        [SerializeField] private Transform pointerArrow;
        [SerializeField] private Transform bubbleSpawnPoint;
        
        public PlayerMoveState MoveState;
        public PlayerAttackState AttackState;
        public PlayerIdleState IdleState;
        public PlayerHurtState HurtState;
        public PlayerDeathState DeathState;

        private PlayerBaseState CurrentState { get; set; }
        public PlayerAnimator Animator { get; set; }
        public InputManager InputManager { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public CountdownTimer AttackTimer { get; private set; }
        public float CurrentSpeed { get; set; }

        private Camera _mainCamera;
        private Vector2 _direction;
        private int _currentHealth;
        private int _damageTaken;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            InputManager = GetComponent<InputManager>();
            Bobber = GetComponentInChildren<PlayerBobber>();
            Animator = GetComponentInChildren<PlayerAnimator>();

            _mainCamera = Camera.main;
            AttackTimer = new CountdownTimer(PlayerStats.AttackCooldown);
            AttackTimer.Start();

            _currentHealth = PlayerStats.MaxHealth;

            CurrentSpeed = PlayerStats.MaxSpeed;

            InitStates();
            ChangeState(IdleState);
        }
        
        private void Update()
        {
            if (!HasDied())
                Animator.HandleMoveAnimation(InputManager.Movement);

            AttackTimer?.Tick(Time.deltaTime);
            CurrentState.UpdateState();
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdateState();

            if (!HasDied())
                SetPointerArrow(InputManager.MousePos);
        }

        private void SetPointerArrow(Vector2 mouseInput)
        {
            var mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 0f));
            _direction = mousePosition - transform.position;

            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            var targetRotation = Quaternion.Euler(0f, 0f, angle);
            pointerArrow.rotation = Quaternion.Slerp(pointerArrow.rotation, targetRotation,
                Time.fixedDeltaTime * PlayerStats.ArrowFollowSpeed);
        }

        public void SpawnBullet()
        {
            if (!PlayerStats.BubbleBulletPrefab)
            {
                Debug.LogError("PlayerStats.BubbleBulletPrefab is null");
                return;
            }

            _currentHealth -= 1;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);

            PlayerShrinker.HandleShrink(false);
            CurrentSpeed = PlayerShrinker.HandleSpeed(false);
            Debug.Log("Player Speed:" + CurrentSpeed);

            var bubble = Instantiate(PlayerStats.BubbleBulletPrefab, bubbleSpawnPoint.position, Quaternion.identity);
            bubble.Init(_direction);

            Event.OnPlayerHealthChange(_currentHealth);
        }

        public void TakeDamage()
        {
            PlayerShrinker.HandleShrink(false);
            CurrentSpeed = PlayerShrinker.HandleSpeed(false);
            
            _currentHealth -= _damageTaken;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);
            Event.OnPlayerHealthChange(_currentHealth);
        }

        public void TakeDamage(int newDamage)
        {
            _currentHealth -= newDamage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);
            Event.OnPlayerHealthChange(_currentHealth);
        }


        private void Refill(int restoreAmount)
        {
            // Refill health first
            if (_currentHealth < PlayerStats.MaxHealth)
            {
                _currentHealth += restoreAmount;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);


                PlayerShrinker.HandleShrink(true, _currentHealth);
                CurrentSpeed = PlayerShrinker.HandleSpeed(true, _currentHealth);
                Debug.Log("Player Speed:" + CurrentSpeed);

                Event.OnPlayerHealthChange(_currentHealth);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ICollectable collectable)) return;

            Refill(collectable.RestoreAmount());
            collectable.Collect();
        }

        #region State Machine

        private void InitStates()
        {
            MoveState = new PlayerMoveState(this);
            AttackState = new PlayerAttackState(this);
            IdleState = new PlayerIdleState(this);
            HurtState = new PlayerHurtState(this);
            DeathState = new PlayerDeathState(this);
        }

        public void HandleAttack()
        {
            if (HasALife())
                CurrentState.HandleAttack();
        }

        public void GetDamaged(float damage)
        {
            _damageTaken = Mathf.RoundToInt(damage);
            ChangeState(HurtState);
        }

        public void ChangeState(PlayerBaseState newState)
        {
            if (CurrentState == newState) return;

            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        #endregion

        #region Player Checks

        public bool HasDied()
        {
            return _currentHealth <= 0;
        }

        public bool HasALife()
        {
            return _currentHealth > 1;
        }

        #endregion
        
    }
}