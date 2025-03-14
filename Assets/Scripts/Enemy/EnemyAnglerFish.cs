using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Enemy
{
    public class EnemyAnglerFish : EnemyBase
    {
        [SerializeField] private Sprite shoot, normal;

        [Header("Shoot Parameters")] [SerializeField]
        private EnemyBullet enemyBullet;

        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootDelay;
        [SerializeField] private float chargeDuration;
        [SerializeField] private Ease ease = Ease.Flash;
        [SerializeField] private float chargeScale = 1.5f;

        [SerializeField] private AudioClip[] pewSounds;
       
        private Tween _shootTween;

        public override void SetSpriteNormal()
        {
            _spriteRenderer.sprite = normal;
        }

        public override void ChargeUpAttack()
        {
            _spriteRenderer.sprite = chargeUp;

            ChargeTween?.Kill();
            ChargeTween = _spriteRenderer.transform.DOScale(chargeScale, chargeDuration)
                .SetEase(ease)
                .OnComplete(() => { ChargeUpFinished = true; });
        }

        public override void AttackPlayer()
        {
            _spriteRenderer.flipX = Detector.GetPlayerDirection().x > 0;

            _spriteRenderer.sprite = shoot;
            IsAttacking = true;
// Get player direction
            Vector2 playerDirection = Detector.GetPlayerDirection();

// Shoot bullet
            if (AudioManager.Instance)
                AudioManager.Instance.PlayAudioSfx(pewSounds[Random.Range(0, pewSounds.Length)]);

            var bullet = Instantiate(enemyBullet, shootPoint.position, Quaternion.identity);
            bullet.Init(playerDirection);

            _shootTween?.Kill();
            _shootTween = _spriteRenderer.transform.DOScale(1, shootDelay)
                .OnComplete(() =>
                {
                    ChargeUpFinished = false;
                    ChangeState(ChaseState);
                    IsAttacking = false;
                });
        }

        private void OnDestroy()
        {
            _shootTween?.Kill();
        }
    }
}