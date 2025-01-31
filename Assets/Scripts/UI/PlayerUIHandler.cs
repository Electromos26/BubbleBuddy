using System;
using DG.Tweening;
using Events;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIHandler : MonoBehaviour
    {
        public GameEvent Event;
        public PlayerStats PlayerStats;
        [Header("Health UI")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private float healthSmoothSpeed = 5f;

        [Header("Shake Effect")] 
        [SerializeField] private float shakeDuration = 0.5f;
        [SerializeField] private float shakeStrength = 0.5f;
        
        private Tween _shakeTween;

        private void Awake()
        {
            if (healthSlider == null)
                healthSlider = GetComponentInChildren<Slider>();

            healthSlider.maxValue = PlayerStats.MaxHealth;
        }

        private void OnEnable()
        {
            Event.OnPlayerHealthChange += UpdateHealthUI;
            Event.OnPlayerHit += ShakeHealthUI;
        }

        private void OnDisable()
        {
            Event.OnPlayerHealthChange -= UpdateHealthUI;
            Event.OnPlayerHit -= ShakeHealthUI;
        }


        private void UpdateHealthUI(float playerHealth)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, playerHealth, Time.deltaTime * healthSmoothSpeed);
        }
        
        private void ShakeHealthUI()
        {
            _shakeTween?.Kill();
            _shakeTween = transform.DOShakePosition(shakeDuration, shakeStrength);
        }
    }
}