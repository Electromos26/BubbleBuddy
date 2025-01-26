using DG.Tweening;
using Managers;
using UnityEngine;

namespace Enemy
{
    public class EnemyPufferFish : EnemyBase
    {
        [Header("Attack Effect")] 
        [SerializeField] private float scaleStrength = 2f;
        [SerializeField] private float duration = 1.5f;
        
        [SerializeField] private GameObject explodeEffect;
        
        [SerializeField] private AudioClip popSound;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Pufferfish triggered with: {other.gameObject.name}");
        }

        public override void ChargeUpAttack()
        {
            _spriteRenderer.transform.DOShakePosition(duration, scaleStrength)
                .OnComplete(() => { ChargeUpFinished = true; });
        }

        public override void AttackPlayer()
        {
            // Kill all existing tweens before state change
            KillAllTweens();
        
            if (explodeEffect)
                Instantiate(explodeEffect, transform.position, Quaternion.identity);
            
            AudioManager.Instance.PlayAudioSfx(popSound);

            if (Detector.PlayerInRange && Detector.Player != null)
            {
                Detector.Player.GetDamaged(damage);
                Detector.PlayerInRange = false;
            }

            ChangeState(DeathState);
        }

        private void KillAllTweens()
        {
            transform.DOKill();
            if (_spriteRenderer != null && _spriteRenderer.transform != null)
                _spriteRenderer.transform.DOKill();
        }
    }
}