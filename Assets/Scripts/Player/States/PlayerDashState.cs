using UnityEngine;
using Utilities;

namespace Player.States
{
    public class PlayerDashState : PlayerBaseState
    {
        public PlayerDashState(PlayerController playerController) : base(playerController)
        {
            _cooldownTimer = playerController.DashTimer;
        }

        private readonly CountdownTimer _cooldownTimer;

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void FixedUpdateState()
        {
            if (_cooldownTimer.IsFinished)
            {
                HandleMovement(InputManager.Movement);
            }

            Player.ChangeState(Player.IdleState);
            
        }
        public override void HandleMovement(Vector2 direction)
        {
            _cooldownTimer.Reset(PlayerStats.DashCooldown); //timer logic 
            _cooldownTimer.Start();

            Rb.AddForce(direction * PlayerStats.DashForce, ForceMode2D.Impulse);
        }
    }
}