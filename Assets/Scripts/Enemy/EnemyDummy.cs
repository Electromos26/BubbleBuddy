using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyDummy : EnemyBase
    {
        public override void ChargeUpAttack()
        {
            AttackPlayer();
        }

        public override void AttackPlayer()
        {
            
        }

        public override void GetDamaged(float damage)
        {
            base.GetDamaged(damage);
            GetHitTween?.Kill();
            GetHitTween = transform.DOShakeScale(0.5f, 0.5f);
            Debug.Log("AHHHHH");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable player) && other.CompareTag("Player"))
            {
                player.GetDamaged(damage);
            }
        }
    }
}