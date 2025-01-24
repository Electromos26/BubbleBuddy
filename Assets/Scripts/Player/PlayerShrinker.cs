using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerShrinker : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Ease ease = Ease.InOutBounce;

        [field: SerializeField] public float[] ShrinkSizes { get; private set; } = new float[4] { 0.25f, 0.5f, 0.75f, 1.0f };

        private int _currentSize;
        private Tween _srinkTween;

        private void Awake()
        {
            _currentSize = ShrinkSizes.Length - 1;
        }

        public void HandleShrink(bool grow)
        {
            _currentSize += grow ? 1 : -1;
            _currentSize = Mathf.Clamp(_currentSize, 0, ShrinkSizes.Length - 1);

            _srinkTween?.Kill();
            _srinkTween = transform.DOScale(ShrinkSizes[_currentSize], duration).SetEase(ease);
        }
    }
}