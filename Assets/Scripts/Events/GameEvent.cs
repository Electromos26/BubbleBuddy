using UnityEngine;
using UnityEngine.Events;
using Enemy;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameSO/GameEvent", order = 0)]
public class GameEvent : ScriptableObject
{
    public UnityAction<EnemyBase> OnEnemyDied;
        
    public UnityAction<float> OnPlayerHealthChange;

    public UnityAction EndGame;
    
    public UnityAction<int> FinalScore;

    public int Score;
}
