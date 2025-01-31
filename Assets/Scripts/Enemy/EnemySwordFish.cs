using System;
using DG.Tweening;
using Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySwordFish : EnemyBase
    {
        [SerializeField] private float stunTime;
        [SerializeField] private float lookSpeed;

        [Header("Jump Parameters")] [SerializeField]
        private float jumpDistance;

        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpDelay;


        [SerializeField] private Sprite dash, follow, stun;

        [SerializeField] private AudioClip[] slashSound;

        private Tween _jumpTween;
        private Camera cam;

        protected override void Awake()
        {
            StunTimer = new CountdownTimer(stunTime);
            base.Awake();
            cam = Camera.main;
        }

        public override void ChargeUpAttack()
        {
            _spriteRenderer.sprite = chargeUp;
            ChargeUpFinished = true;
        }

        protected override void Update()
        {
            base.Update();
            StunTimer?.Tick(Time.deltaTime);
            if (Detector.PlayerInRange && IsAttacking)
            {
                Detector.Player.GetDamaged(damage);
                Detector.PlayerInRange = false;
            }

            if (CurrentState == ChaseState) LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            // Get the direction to the player
            var direction = Detector.GetPlayerDirection();
        
            // Calculate the angle in degrees
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            // Create the target rotation
            var targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        
            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }

        public override void AttackPlayer()
        {
            _spriteRenderer.sprite = dash;
            IsAttacking = true;
            var distance = transform.position + Detector.GetPlayerDirection() * jumpDistance;
            _jumpTween?.Kill();
            _jumpTween = transform.DOMove(distance, jumpDuration)
                .SetDelay(jumpDelay)
                .OnComplete(() =>
                {
                    AudioManager.Instance.PlayAudioSfx(slashSound[Random.Range(0, slashSound.Length)]);
                    IsAttacking = false;
                    ChangeState(StunState);
                    _spriteRenderer.sprite = stun;
                });
        }


        private void OnDestroy()
        {
            _jumpTween?.Kill();
        }

        public override void SetSpriteNormal()
        {
            _spriteRenderer.sprite = follow;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.red;

            // Calculate the circle's center position
            Vector3 circleCenter = transform.position + Detector.GetPlayerDirection() * jumpDistance;

            // Draw the circle
            Gizmos.DrawWireSphere(circleCenter, 0.5f);
        }
    }
}