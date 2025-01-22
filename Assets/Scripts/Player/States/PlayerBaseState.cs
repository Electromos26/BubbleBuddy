using UnityEngine;

namespace Player.States
{
    public abstract class PlayerBaseState 
    {
        protected PlayerBaseState(PlayerController playerController)
        {
            Player = playerController;
            PlayerStats = playerController.PlayerStats;
            Rb = playerController.Rb;
        }
        protected PlayerController Player { get; private set; }
        protected PlayerStats PlayerStats;
        protected Rigidbody2D Rb;

        public abstract void EnterState();
        public abstract void ExitState();
    
        public virtual void UpdateState(){}
        public virtual void FixedUpdateState(){}
    
        public virtual void HandleMovement(Vector2 direction) { }
        public virtual void HandleAttack(bool isHeld) { }
        public virtual void HandleDash(bool isHeld) { }
    }
}
