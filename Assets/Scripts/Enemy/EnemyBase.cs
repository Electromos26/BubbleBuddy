using System;
using DG.Tweening;
using Enemy.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected Collider2D damageCollider;


        protected EnemyIdleState IdleState;
        protected EnemyPatrolState PatrolState;
        public EnemyAttackState AttackState;
        public EnemyChaseState ChaseState;

        protected EnemyBaseState CurrentState;
        public EnemyAnimator Animator { get; set; }
        public PlayerDetector Detector { get; set; }
        protected Tween GetHitTween;
        
        private float _currentHealth;

        protected virtual void Awake()
        {
           Init();
           ChangeState(IdleState);
        }

        private void Update()
        {
            CurrentState?.UpdateState();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdateState();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out IDamageable damageable);
            damageable?.GetDamaged(damage);
            ChangeState(IdleState);
        }

        #region EnemyStates

        public void ChangeState(EnemyBaseState newState)
        {
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        #endregion

        private void Init()
        {
            AttackState = new EnemyAttackState(this);
            IdleState = new EnemyIdleState(this);
            PatrolState = new EnemyPatrolState(this); 
            ChaseState = new EnemyChaseState(this);
        }
        public virtual void GetDamaged(float damage)
        {
            _currentHealth -= damage;
        }

        public void FollowPlayer()
        {
            //transform.DOMove(_player)
        }
    }
}