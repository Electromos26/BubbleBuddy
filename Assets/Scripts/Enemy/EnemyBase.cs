using DG.Tweening;
using Enemy.States;
using UnityEngine;
using Managers;
using Utilities;

namespace Enemy
{
    [RequireComponent(typeof(PlayerDetector), typeof(Collider2D))]
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        public GameEvent Event;
        [SerializeField] protected float damage;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float speed;
        [field: SerializeField] public float Points { get; protected set; }

        [Header("Hit Effect")] 
        [SerializeField] protected ParticleSystem hitEffect;

        [SerializeField] protected float hitDuration, hitStrength;


        public EnemyChaseState ChaseState;
        public EnemyAttackState AttackState;
        public EnemyDeathState DeathState;
        public EnemyStunState StunState;
       
        protected EnemyBaseState CurrentState;

        public PlayerDetector Detector { get; set; }
        public CountdownTimer StunTimer{ get; protected set; }
        protected Tween GetHitTween;

        public bool ChargeUpFinished { get; protected set; }
        public bool IsHit { get; private set; } 
        public bool IsAttacking { get; set; }

        protected SpriteRenderer _spriteRenderer;
        protected float _currentHealth;

        
        private Tween _hitTween;
        private Tween _deathTween;
        protected Tween ChargeTween;
        protected Tween AttackTween;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Detector = GetComponent<PlayerDetector>();
            _currentHealth = maxHealth;
            
            Init();
            
            ChangeState(ChaseState);
        }

        protected virtual void Update()
        {
            CurrentState?.UpdateState();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdateState();
        }

        public void FollowPlayer()
        {
            transform.position += Detector.GetPlayerDirection() * speed * Time.deltaTime;

            _spriteRenderer.flipX = Detector.GetPlayerDirection().x > 0;
        }

        public abstract void ChargeUpAttack();

        public abstract void AttackPlayer();

        public virtual void PlayDeathAnimation()
        {
            if (hitEffect)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            _deathTween = transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.Flash).OnComplete(() => { Destroy(gameObject); });
        }


        #region EnemyStates

        public void ChangeState(EnemyBaseState newState)
        {
            if(CurrentState == newState) return;
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        #endregion

        protected void Init()
        {
            AttackState = new EnemyAttackState(this);
            DeathState = new EnemyDeathState(this);
            ChaseState = new EnemyChaseState(this);
            StunState = new EnemyStunState(this);
        }

        public virtual void GetDamaged(float damage)
        {
            _currentHealth -= damage;
            
            IsHit = true;

            _hitTween?.Kill();
            _hitTween = transform.DOShakeRotation(hitDuration, hitStrength).OnComplete(() =>
            {
                if (_currentHealth <= 0)
                {

                   Event.OnEnemyDied?.Invoke(this);
                   ChangeState(DeathState);
                }
                else
                {
                    IsHit = false;
                }
            });
        }

        private void OnDestroy()
        {
            _hitTween?.Kill();
            _deathTween?.Kill();
            ChargeTween?.Kill();
            AttackTween?.Kill();
        }
    }
}