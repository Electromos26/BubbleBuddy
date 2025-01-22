using System;
using UnityEngine;

namespace Player.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerController playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            if (Player.InputManager.Movement == Vector2.zero)
            {
                Player.ChangeState(Player.IdleState);
            }
        }

        public override void FixedUpdateState()
        {
            HandleMovement(Player.InputManager.Movement);
        }

        public override void HandleMovement(Vector2 direction)
        {
            Rb.AddForce(direction * PlayerStats.Acceleration, ForceMode2D.Force);

            if (Rb.linearVelocity.magnitude > PlayerStats.MaxSpeed)
            {
                Rb.linearVelocity = Rb.linearVelocity.normalized * PlayerStats.MaxSpeed;
            }
        }
    }
}