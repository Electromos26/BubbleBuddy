using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameSO/GameEvent", order = 0)]
public class GameEvent : ScriptableObject
{
    public static UnityAction OnPlayerAttack;
}


