using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
using Utils;

namespace Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        public event Action<int> OnWaveComplete;
        public event Action<int> OnWaveStart;
        public float GameTime { get; private set; }
        
        [SerializeField] private TextMeshProUGUI waveText;
        
        [Header("Wave Timing")]
        [SerializeField] private float waveDuration = 30f;
        [SerializeField] private float waveBreakDuration = 5f;
        
        [Header("Wave Timing")]
        [SerializeField] private float tweenDuration = 30f;
        [SerializeField] private Vector2 tweenPos = Vector2.zero;
        
        [Header("Wave Enemy Counts")]
        [SerializeField] private int[] enemiesPerWave = new int[] { 2, 3, 5, 7 };
        
        private float waveTimer;
        private float breakTimer;
        private int currentWave = 0;
        private bool isWaveActive = false;
        
        private Tween wavebannerTween;
        private Vector2 waveStartPos;
        private RectTransform rectTransform;
       

        private void Start()
        {
            rectTransform  = GetComponent<RectTransform>();
            waveStartPos = rectTransform.anchoredPosition;
            BannerAnimateIn();
        }

        public void BannerAnimateIn()
        {
            waveText.text = "Wave: " + currentWave.ToString();
            wavebannerTween?.Kill();
            wavebannerTween = rectTransform.DOAnchorPos(tweenPos, tweenDuration).SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    StartNewWave();
                    BannerAnimateOut();
                });
        }

        public void BannerAnimateOut()
        {
            wavebannerTween?.Kill();
            wavebannerTween = rectTransform.DOAnchorPos(waveStartPos, tweenDuration).SetEase(Ease.InOutSine);
        }
        private void Update()
        {
            if (isWaveActive)
            {
                GameTime += Time.deltaTime;
                waveTimer -= Time.deltaTime;
                if (waveTimer <= 0)
                {
                    CompleteWave();
                }
            }
            else
            {
                breakTimer -= Time.deltaTime;
                if (breakTimer <= 0)
                {
                    BannerAnimateIn();
                }
            }
        }

        private void StartNewWave()
        {
            currentWave++;
            waveTimer = waveDuration;
            isWaveActive = true;
            OnWaveStart?.Invoke(currentWave);
        }

        private void CompleteWave()
        {
            isWaveActive = false;
            breakTimer = waveBreakDuration;
            OnWaveComplete?.Invoke(currentWave);
        }

        public int GetMaxEnemiesForCurrentWave()
        {
            int index = Mathf.Min(currentWave - 1, enemiesPerWave.Length - 1);
            return enemiesPerWave[index];
        }

        public float GetWaveProgress() => waveTimer / waveDuration;
        public int GetCurrentWave() => currentWave;
        public bool IsWaveActive() => isWaveActive;
    }
}