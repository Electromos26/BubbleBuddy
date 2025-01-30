using System;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIHandler : MonoBehaviour
    {
        public GameEvent Event;
        //[Header("Health UI")] [SerializeField] private GameObject[] _heartContainers;
        
        [SerializeField] private Slider healthSlider;
 
        private int _currentHealth;

        private void OnEnable()
        {
           // Event.OnPlayerHealthChange += UpdateHealthUI;
        }

        private void UpdateHealth()
        {
            
        }
        
        
/**/        /*public void UpdateHealthUI(float playerHealth)
        {
            int newHealth = Mathf.Clamp(Mathf.RoundToInt(playerHealth), 0, _heartContainers.Length);
            
            if (_currentHealth == newHealth) return;

            _currentHealth = newHealth;
            
            for (var i = 0; i < _heartContainers.Length; i++)
            {
                _heartContainers[i].SetActive(i < _currentHealth);
            }
        }*/



        public void RoundNumber(string numTxt)
        {
            int num = Convert.ToInt32(numTxt);
            
                int rem = num % 10;
                
                  //  displayText.text =  (num - rem).ToString();
        }
    }
}