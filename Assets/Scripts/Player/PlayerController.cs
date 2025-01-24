using System;
using UnityEngine;
using Player.States;
using UI;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour , IDamageable
    {
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public PlayerBobber Bobber { get; private set; }
    [field: SerializeField] public PlayerUIHandler UIHandler { get; private set; }
    [field: SerializeField] public PlayerShrinker PlayerShrinker { get; private set; }
    [SerializeField] private Transform pointerArrow;
    [SerializeField] private Transform bubbleSpawnPoint;

    public PlayerMoveState MoveState;
    public PlayerAttackState AttackState;
    public PlayerIdleState IdleState;
    public PlayerHurtState HurtState;

    //public PlayerDeathState deathState;
    //public PlayerDashState dashState;


    private PlayerBaseState CurrentState { get; set; }
    public PlayerAnimator Animator { get; set; }
    public InputManager InputManager { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public CountdownTimer AttackTimer { get; private set; }

    private Camera _mainCamera;
    private Vector2 _direction;
    private int _currentBullet;
    private float _currentHealth;
    private float _damageTaken;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        InputManager = GetComponent<InputManager>();
        Bobber = GetComponentInChildren<PlayerBobber>();
        Animator = GetComponentInChildren<PlayerAnimator>();

        _mainCamera = Camera.main;
        AttackTimer = new CountdownTimer(PlayerStats.AttackCooldown);
        AttackTimer.Start();

        _currentBullet = PlayerStats.MaxBubbleBulletAmount;
        _currentHealth = PlayerStats.MaxHealth;
        UIHandler.UpdateAmmoUI(_currentBullet);
        UIHandler.UpdateHealthUI(_currentHealth);
        InitStates();
        ChangeState(IdleState);
    }

    private void Update()
    {
        Animator.HandleMoveAnimation(InputManager.Movement);
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
            Time.fixedDeltaTime * PlayerStats.ArrowFollowSpeed);
    }

    public void SpawnBullet()
    {
        if (!PlayerStats.BubbleBulletPrefab)
        {
            Debug.LogError("PlayerStats.BubbleBulletPrefab is null");
            return;
        }

        _currentBullet--;

        var bubble = Instantiate(PlayerStats.BubbleBulletPrefab, bubbleSpawnPoint.position, Quaternion.identity);
        bubble.Init(_direction);

        if (UIHandler == null)
        {
            Debug.LogError("PlayerUIHandler is Missing add ref to PlayerController");
            return;
        }

        UIHandler.UpdateAmmoUI(_currentBullet);
    }

    public void TakeDamage()
    {
       _currentHealth -= _damageTaken;
       _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);
       UIHandler.UpdateHealthUI(_currentHealth);
    }

    private void Refill(CollectableData data)
    {
        _currentHealth += data.HealData;
        _currentBullet += data.AmmoData;
        
        _currentHealth = Mathf.Clamp(_currentHealth, 0, PlayerStats.MaxHealth);
        _currentBullet = Mathf.Clamp(_currentBullet, 0, PlayerStats.MaxBubbleBulletAmount);
        
        PlayerShrinker.HandleShrink(true);
        
        UIHandler.UpdateHealthUI(_currentHealth);
        UIHandler.UpdateAmmoUI(_currentBullet);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            Refill(collectable.GetData());
            collectable.Collect();
        }
    }

    #region State Machine

    private void InitStates()
    {
        MoveState = new PlayerMoveState(this);
        AttackState = new PlayerAttackState(this);
        IdleState = new PlayerIdleState(this);
        HurtState = new PlayerHurtState(this);
    }

    public void HandleAttack()
    {
        CurrentState.HandleAttack();
    }
    
    public void GetDamaged(float damage)
    {
        _damageTaken = damage;
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
    
    public bool HasBubbleBullet()
    {
        return _currentBullet > 0;
    }

    public bool HasDied()
    {
        return _currentHealth <= 0;
    }

    #endregion
    
    }
}