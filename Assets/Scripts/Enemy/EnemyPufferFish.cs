using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Enemy
{
    public class EnemyPufferFish : EnemyBase
    {
        [Header("Attack Effect")] [SerializeField]
        private float scaleStrength = 2f;

        [SerializeField] private float duration = 1.5f;

        [SerializeField] private GameObject explodeEffect;

        [SerializeField] private AudioClip popSound;
        
        public override void ChargeUpAttack()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = chargeUp;
                ChargeTween = _spriteRenderer.transform.DOShakePosition(duration, scaleStrength)
                    .OnComplete(() => { ChargeUpFinished = true; });
            }
        }

        public override void AttackPlayer()
        {
           ChargeTween?.Kill();

            if (explodeEffect)
                Instantiate(explodeEffect, transform.position, Quaternion.identity);
            if (AudioManager.Instance)
                AudioManager.Instance.PlayAudioSfx(popSound);

            if (Detector.PlayerInRange && Detector.Player != null)
            {
                Detector.Player.GetDamaged(damage);
                Detector.PlayerInRange = false;
            }

            Event.OnEnemyDied?.Invoke(this);
            ChangeState(DeathState);
        }
        
        
        private void OnDestroy()
        {
           ChargeTween?.Kill(); 
        }
    }
}