using UnityEngine;
using DG.Tweening;
using Events;
using Managers;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public GameEvent Event;
        [SerializeField] private float tiltAngle = 90f;
        [SerializeField] private float duration = 0.5f;

        [SerializeField] public ParticleSystem deathEffect;

        [SerializeField] private AudioClip dieSfx;


        private SpriteRenderer _spriteRenderer;
        private Vector3 _leftRotation;
        private Vector3 _rightRotation;
        private Vector2 _previousInput;
        
        private Tween _rotateTween;
        private Tween _deathTween;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _leftRotation = new Vector3(0f, 0f, tiltAngle);
            _rightRotation = new Vector3(0f, 0f, -tiltAngle);
        }

        public void HandleMoveAnimation(Vector2 input)
        {
            if (_previousInput == input) return;

            _rotateTween?.Kill();

            if (input == Vector2.left || input == Vector2.right)
            {
                _spriteRenderer.flipX = input == Vector2.left;
                _rotateTween = transform.DORotate(input == Vector2.left ? _leftRotation : _rightRotation, duration);
            }
            else if (input == Vector2.up || input == Vector2.down || input == Vector2.zero)
            {
                _rotateTween = transform.DORotate(Vector3.zero, duration);
            }

            _previousInput = input;
        }

        public void HandleDeathAnimation()
        {
            _rotateTween?.Kill();
            AudioManager.Instance.PlayAudioSfx(dieSfx);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            _deathTween = transform.DOScale(Vector3.zero, duration).OnComplete(() => { Event.OnEndGame?.Invoke(); });
            _rotateTween = transform.DORotate(Vector3.one, duration).SetLoops(-1, LoopType.Yoyo);
        }
        
        private void OnDestroy()
        {
            _rotateTween?.Kill();
            _deathTween?.Kill();
        }
    }
}