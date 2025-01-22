using UnityEngine;
using Utilities;

namespace Player.States
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerController playerController) : base(playerController)
        {
            _attackTimer = playerController.AttackTimer;
        }

        private readonly CountdownTimer _attackTimer;

        public override void EnterState()
        {
            if (_attackTimer.IsFinished)
            {
                FireBubble();
            }

            Player.ChangeState(Player.IdleState);
        }

        public override void ExitState()
        {
        }

        private void FireBubble()
        {
            // Attack logic
            _attackTimer.Reset(PlayerStats.AttackCooldown);
            _attackTimer.Start();
            Player.Bobber.Shake();
            Debug.Log("Pew pew");
        }
    }
}