using UnityEngine;
using Utilities;

namespace Player.States
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerController playerController) : base(playerController)
        {
            _cooldownTimer = playerController.AttackTimer;
        }

        private readonly CountdownTimer _cooldownTimer;

        /// <summary>
        /// if cooldown is finished fire bubble and rest timer 
        /// </summary>
        public override void EnterState()
        {
            if (_cooldownTimer.IsFinished)
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
            _cooldownTimer.Reset(PlayerStats.AttackCooldown); //timer logic 
            _cooldownTimer.Start();

            Player.Bobber.Shake(); //Shoot Effect on player

            Player.SpawnBullet();
        }
    }
}