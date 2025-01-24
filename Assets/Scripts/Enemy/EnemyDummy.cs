using System;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyDummy : EnemyBase, IDamageable
    {
        public void GetDamaged(float damage)
        {
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