using System;
using Enemy;
using Events;
using Managers;
using TMPro;
using UnityEngine;


namespace UI
{
    public class ScoreUIHandler : MonoBehaviour
    {
        public GameEvent Event;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private int _score;
        private int _highScore;

        private void Awake()
        {
            _score = 0;
            PlayerPrefs.GetInt("HighScore", _highScore);
            
            scoreText.text = _score.ToString();
            highScoreText.text = _highScore.ToString();
        }

        private void OnEnable()
        {
            Event.OnEnemyDied += SetScore;
            Event.OnEndGame += SetScore;
        }

        private void SetScore()
        {
            Event.Score = _score;
        }


        private void SetScore(EnemyBase score)
        {
            _score += (int)score.Points;
            Event.Score = _score;
            scoreText.text = _score.ToString();

            if (_score < _highScore || _highScore >= PlayerPrefs.GetInt("HighScore")) return;
            
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        
    }
}