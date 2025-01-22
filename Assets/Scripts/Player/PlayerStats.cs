using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "GameSO/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        [field: Header("Movement")]
        [field: SerializeField] public float Acceleration { get; private set; } 
        [field: SerializeField] public float Deceleration { get; private set; } 
        [field: SerializeField] public float MaxSpeed { get; private set; } 
        //public float dashSpeed = 10f;

        [field: Header("Combat")]
        [field: SerializeField] public float AttackCooldown {get ; private set; }
        [field: SerializeField] public BubbleBullet BubbleBulletPrefab { get; private set; }

        [field: Header("Pointer Arrow")]
        [field: SerializeField] public float ArrowSpeed { get; private set; }
        
        //[field: Header("Health")]
        // public float maxHealth = 10f;
    }
}