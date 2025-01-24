using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerShrinker : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Ease ease = Ease.InOutBounce;

        [field: SerializeField] public float[] ShrinkSizes { get; private set; } = new float[4] { 0.25f, 0.5f, 0.75f, 1.0f };
        [field: SerializeField] public float[] ShrinkSpeeds { get; private set; } = new float[5] { 15f, 14f, 13f, 12f,10f };

        private int _currentSize;
        private int _currentSpeed;
        private Tween _srinkTween;

        private void Awake()
        {
            _currentSize = ShrinkSizes.Length - 1;
            _currentSpeed = ShrinkSpeeds.Length - 1;
        }

        public void HandleShrink(bool grow, int growAmount = 1)
        {
            _currentSize += grow ? growAmount : -growAmount;
            _currentSize = Mathf.Clamp(_currentSize, 0, ShrinkSizes.Length - 1);

            _srinkTween?.Kill();
            _srinkTween = transform.DOScale(ShrinkSizes[_currentSize], duration).SetEase(ease); 
        }

        public float HandleSpeed(bool grow, int growAmount = 1)
        {
            _currentSpeed += grow ? growAmount : -growAmount;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0 , ShrinkSpeeds.Length - 1);
            
            return ShrinkSpeeds[_currentSpeed];
        }
    }
}