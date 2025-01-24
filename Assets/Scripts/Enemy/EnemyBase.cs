using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Collider2D damageCollider;
        [SerializeField] protected float damage;
        protected Tween GetHitTween;
        
    }
}
