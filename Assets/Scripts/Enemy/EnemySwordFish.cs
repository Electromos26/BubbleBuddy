using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class EnemySwordFish : EnemyBase
    {
        [SerializeField] private float stunTime;
        [Header("Jump Parameters")]
        [SerializeField] private float jumpDistance;
        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpDelay;

        private Tween _jumpTween;

        protected override void Awake()
        {
            StunTimer = new CountdownTimer(stunTime);
            base.Awake();
        }

        public override void ChargeUpAttack()
        {
            ChargeUpFinished = true;
        }

        protected override void Update()
        {
            base.Update();
           StunTimer?.Tick(Time.deltaTime); 
        }

        public override void AttackPlayer()
        {
            IsAttacking = true;
            var distance = transform.position + Detector.GetPlayerDirection() * jumpDistance;
            _jumpTween?.Kill();
            _jumpTween = transform.DOMove(distance, jumpDuration)
                .SetDelay(jumpDelay)
                .OnComplete(() => IsAttacking = false);
            if (Detector.PlayerInRange)
            {
                Detector.Player.GetDamaged(damage);
                Detector.PlayerInRange = false;
            }

            ChangeState(StunState);
        }

        private void OnDestroy()
        {
            _jumpTween?.Kill();
        }

        private void OnDrawGizmos()
        {
            if(!Application.isPlaying) return;
            Gizmos.color = Color.red;

            // Calculate the circle's center position
            Vector3 circleCenter = transform.position + Detector.GetPlayerDirection() * jumpDistance;

            // Draw the circle
            Gizmos.DrawWireSphere(circleCenter, 0.5f);
        }
    }
}