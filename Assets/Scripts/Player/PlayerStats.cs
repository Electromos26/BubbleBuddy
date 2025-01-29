using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "GameSO/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        [field: Header("Movement")]
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float Deceleration { get; private set; } 
        [field: SerializeField] public float DashForce { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        //public float dashSpeed = 10f;

        [field: Header("Health")]
        [field: SerializeField] public int MaxHealth { get; private set; } = 5;
        
        [field: Header("Combat")]
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float DashCooldown { get; private set; }
        [field: SerializeField] public int MaxBubbleBulletAmount { get; private set; }
        [field: SerializeField] public BubbleBullet BubbleBulletPrefab { get; private set; }

        [field: Header("Pointer Arrow")]
        [field: SerializeField] [Min(0.1f)] public float ArrowFollowSpeed { get; private set; }

    }
}