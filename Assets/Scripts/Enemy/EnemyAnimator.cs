using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Tween _attackTween;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger("Death");
        }

        public void PlayChargeAnimation()
        {
            _animator.SetTrigger("Charge");
        }

        public void FinishedCharging()
        {
            PlayAttackAnimation();
        }
        
        private void PlayAttackAnimation()
        {
          // _attackTween = transform.DOMove(Detector)
        }
    }
}