using UnityEngine;

namespace Player.States
{
    public class PlayerHurtState : PlayerBaseState
    {
        public PlayerHurtState(PlayerController playerController) : base(playerController)
        {
        }


        public override void EnterState()
        {
            Player.TakeDamage();
            Player.Animator.PlayHitEffect();
            CheckPlayerStatus();
        }

        public override void ExitState()
        {
        }

        private void CheckPlayerStatus()
        {
            if (Player.HasDied())
            {
                //Player.ChangeState(Player.DeathState);
            }
            else
            {
                Player.ChangeState(Player.IdleState);
            } 
        }
    }
}                         