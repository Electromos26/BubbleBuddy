using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerBobber : MonoBehaviour
    {
        [Header("Bob Settings")]
        [SerializeField, Range(0.1f, 5f)] private float bobDuration = 0.75f;
        [SerializeField, Range(0.1f, 5f)] private float bobHeight = 0.5f;
        [SerializeField, Range(0.1f, 5f)] private float resetDuration = 0.5f;
        [SerializeField] private Ease ease = Ease.InOutSine;

        [Header("Shake Settings")]
        [SerializeField, Range(0.1f, 5f)] private float shakeDuration = 0.75f;
        [SerializeField, Range(0.1f, 5f)] private float shakeStrength = 1f;
        
        [Header("Shadow Settings")]
        [SerializeField] private Transform shadow;
        [SerializeField] private float shadowScale = 0.5f;


        private Vector3 _startPos;
        private Tween _bobTween;
        private Tween _shadowTween;
        private Tween _shakeTween;

        private void Awake()
        {
            _startPos = transform.localPosition;
        }

        public void StartBob()
        {
            StopBob();

            _bobTween = transform
                .DOLocalMoveY(_startPos.y + bobHeight, bobDuration)
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
            _shadowTween = shadow.DOScaleX(shadowScale, bobDuration)
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopBob()
        {
            _bobTween?.Kill();
            _shadowTween?.Kill();

            transform.DOLocalMove(_startPos, resetDuration).SetEase(Ease.InOutSine);
            shadow.DOScaleX(1f, bobDuration).SetEase(Ease.InOutSine);
        }

        public void Shake()
        {
            _shakeTween?.Kill();
            transform.localScale = Vector3.one;
           _shakeTween = transform.DOShakeScale(shakeDuration, shakeStrength);
        }
    }
}