using UnityEngine;

namespace Player.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerController playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
          Player.Bobber.StartBob();
        }

        public override void ExitState()
        {
            Player.Bobber.StopBob();
        }

        public override void UpdateState()
        {
            if (Player.InputManager.Movement != Vector2.zero)
            {
                Player.ChangeState(Player.MoveState);
            }
        }

        public override void FixedUpdateState()
        {
           HandleMovement(Vector2.zero); 
        }
        
        public override void HandleMovement(Vector2 direction)
        {
            Rb.AddForce(Rb.linearVelocity * -PlayerStats.Deceleration, ForceMode2D.Force);   
        }
    }
}