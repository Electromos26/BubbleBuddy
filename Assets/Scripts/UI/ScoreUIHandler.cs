using System;
using Enemy;
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

        public int Score;
        public int HighScore;

        private void Awake()
        {
            Score = 0;
            PlayerPrefs.GetInt("HighScore", HighScore);
            
            scoreText.text = Score.ToString();
            highScoreText.text = HighScore.ToString();
        }

        private void OnEnable()
        {
            Event.OnEnemyDied += SetScore;
            Event.EndGame += SetScore;
        }

        public void SetScore()
        {
            Event.FinalScore?.Invoke(Score);
        }


        private void SetScore(EnemyBase score)
        {
            Score += (int)score.Points;
            scoreText.text = Score.ToString();

            if (Score < HighScore || HighScore >= PlayerPrefs.GetInt("HighScore")) return;
            
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        
    }
}