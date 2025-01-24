using UnityEngine;
using TMPro;

namespace UI
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI ammoText;
        
        public void UpdateAmmoUI(int playerAmmo)
        {
            ammoText.text = playerAmmo.ToString();
        } 
        public void UpdateHealthUI(float playerHealth)
        {
            healthText.text = playerHealth.ToString();
        }
    }
}
