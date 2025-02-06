using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "GameSO/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject
    {
        public UnityAction<EnemyBase> OnEnemyDied;
        
        public UnityAction<float> OnPlayerHealthChange;
        
        
        public UnityAction OnCollectablePickup;
    
        public UnityAction OnPlayerHit;
        public UnityAction OnPlayerDeath;

        public UnityAction OnGameStart;
        public UnityAction OnEndGame;
    
        public UnityAction OnWaveStart;
        public UnityAction OnWaveEnd;

        public int Score;
    }
}
