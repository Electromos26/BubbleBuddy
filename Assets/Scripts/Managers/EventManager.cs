using System;
using UnityEngine.Events;
using Utils;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        
        public UnityAction<float> OnEnemyDied;
        
        public UnityAction<float> OnPlayerHealthChange;
        
    }
}
