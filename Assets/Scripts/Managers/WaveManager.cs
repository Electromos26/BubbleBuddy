using System;
using Events;
using TMPro;
using UnityEngine;
using Utilities;
using Utils;

namespace Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        public GameEvent Event;
        [SerializeField] private UIAnimator waveBannerAnimator;
        [SerializeField] private TextMeshProUGUI waveText;


        [Header("Wave Settings"), Tooltip("The amount of seconds it takes to spawn a wave.")] [SerializeField]
        private float waveBreakInterval = 5f;

        [SerializeField][Min(0.1f)] private float spawnRate = 2f;

        public int CurrentWave { get; private set; }
        public int CurrentEnemyPerWave { get; set; }

        private CountdownTimer _waveTimer;
        private CountdownTimer _spawnTimer;
        private bool _isWaveActive;
        private int _spawnedEnemyCount;


        private void Start()
        {
            CurrentWave = 0;
            _waveTimer = new CountdownTimer(waveBreakInterval);
            _spawnTimer = new CountdownTimer(spawnRate);

          ShowBanner();
        }

        private void OnEnable()
        {
            Event.OnWaveEnd += ShowBanner;
        }

        private void OnDisable()
        {
            Event.OnWaveEnd -= ShowBanner;
        }

        private void Update()
        {
            _waveTimer?.Tick(Time.deltaTime);
            _spawnTimer?.Tick(Time.deltaTime);

            if (!_waveTimer.IsFinished || _isWaveActive) return;

            _isWaveActive = true;
            HideBanner();
        }

        private void FixedUpdate()
        {
            if (_isWaveActive)
                SpawnWaveEnemy();
        }

        private void SpawnWaveEnemy()
        {
            if (CurrentEnemyPerWave <= 0 || _spawnedEnemyCount == CurrentEnemyPerWave) return;

            if (!_spawnTimer.IsFinished) return;
            
            EnemyManager.Instance.SpawnEnemy();
            _spawnTimer.Reset();
            _spawnTimer.Start();
            _spawnedEnemyCount++;

        }

        private void ShowBanner()
        {
            CurrentWave++;
            CurrentEnemyPerWave = (int)Mathf.Pow(2, CurrentWave);
            Debug.Log($"Wave: {CurrentWave} Enemy Count: {CurrentEnemyPerWave}");
            
            waveText.text = $"Wave: {CurrentWave}";
            waveBannerAnimator.FadeInAnimate(true);
            _isWaveActive = false;
            _waveTimer.Reset();
            _waveTimer.Start();
        }

        private void HideBanner()
        {
            _isWaveActive = true;
            waveBannerAnimator.FadeInAnimate(false);
            Event.OnWaveStart?.Invoke();
        }
    }
}