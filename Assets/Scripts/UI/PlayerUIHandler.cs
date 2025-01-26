using UnityEngine;
using TMPro;

namespace UI
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
 
        public void UpdateHealthUI(float playerHealth)
        {
            healthText.text = playerHealth.ToString();
        }
    }
}
