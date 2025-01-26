using UnityEngine;

namespace Player.States
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerController playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            Player.Animator.HandleDeathAnimation();
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            HandleMovement(Vector2.zero);
        }
        
        public override void HandleMovement(Vector2 direction)
        {
            Rb.AddForce(Rb.linearVelocity * -PlayerStats.Deceleration, ForceMode2D.Force);   
        }

    }
}
