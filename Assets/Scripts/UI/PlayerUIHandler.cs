using System;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIHandler : MonoBehaviour
    {
        public GameEvent Event;
        [Header("Health UI")] [SerializeField] private GameObject[] _heartContainers;

        private int _currentHealth;

        private void OnEnable()
        {
            Event.OnPlayerHealthChange += UpdateHealthUI;
        }

        public void UpdateHealthUI(float playerHealth)
        {
            int newHealth = Mathf.Clamp(Mathf.RoundToInt(playerHealth), 0, _heartContainers.Length);
            
            if (_currentHealth == newHealth) return;

            _currentHealth = newHealth;
            
            for (var i = 0; i < _heartContainers.Length; i++)
            {
                _heartContainers[i].SetActive(i < _currentHealth);
            }
        }
    }
}