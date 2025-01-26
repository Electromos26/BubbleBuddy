using UnityEngine;
using UnityEngine.Events;
using Enemy;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameSO/GameEvent", order = 0)]
public class GameEvent : ScriptableObject
{
    public UnityAction<EnemyBase> OnEnemyDied;
        
    public UnityAction<float> OnPlayerHealthChange;
}
