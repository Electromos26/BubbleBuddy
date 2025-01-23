using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyDummy : EnemyBase, IDamageable
    {
        public void GetDamaged(float attackDamage)
        {
            GetHitTween?.Kill();
            GetHitTween = transform.DOShakeScale(0.5f, 0.5f);
            Debug.Log("AHHHHH");
        }
    }
}