using System;
using DG.Tweening;
using Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

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

        
        [SerializeField] private Sprite dash, chargeUp, follow, stun;

        [SerializeField] private AudioClip slashSound;
        
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

            float targetAngle = 0;
            if (direction.y > 0.5f && Mathf.Abs(direction.x) < 0.5f ||
                direction.y < -0.5f && Mathf.Abs(direction.x) < 0.5f)
            {
                targetAngle = -90f;
            }
            else if (direction.x > 0.5f && Mathf.Abs(direction.y) < 0.5f ||
                     direction.x < -0.5f && Mathf.Abs(direction.y) < 0.5f)
            {
                targetAngle = 0f;
            }
            else if (direction.x < 0 && direction.y > 0 || direction.x > 0 && direction.y < 0)
            {
                targetAngle = -30f;
            }
            else if (direction.x < 0 && direction.y < 0 || direction.x > 0 && direction.y > 0)
            {
                targetAngle = 30f;
            }


            var targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                Time.fixedDeltaTime * lookSpeed);
        }

        public override void AttackPlayer()
        {
            _spriteRenderer.sprite = dash;
            AudioManager.Instance.PlayAudioSfx(slashSound);
            IsAttacking = true;
            var distance = transform.position + Detector.GetPlayerDirection() * jumpDistance;
            _jumpTween?.Kill();
            _jumpTween = transform.DOMove(distance, jumpDuration)
                .SetDelay(jumpDelay)
                .OnComplete(() =>
                {
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