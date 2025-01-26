using System;
using Enemy;
using UnityEngine.Events;
using Utils;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        
        public UnityAction<EnemyBase> OnEnemyDied;
        
        public UnityAction<float> OnPlayerHealthChange;
        
    }
}
