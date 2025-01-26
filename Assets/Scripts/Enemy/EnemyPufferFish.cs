using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyPufferFish : EnemyBase
    {
        [Header("Attack Effect")] 
        [SerializeField] private float scaleStrength = 2f;
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private ParticleSystem explodeParticle;

        public override void ChargeUpAttack()
        {
            _spriteRenderer.transform.DOShakePosition(duration,scaleStrength).OnComplete(() => { ChargeUpFinished = true; });
        }

        public override void AttackPlayer()
        {
            if (explodeParticle != null)
                Instantiate(explodeParticle, transform.position, Quaternion.identity);

            if (Detector.PlayerInRange)
            {
                Detector.Player.GetDamaged(damage);
                Detector.PlayerInRange = false;
            }

            ChangeState(DeathState);
        }
    }
}