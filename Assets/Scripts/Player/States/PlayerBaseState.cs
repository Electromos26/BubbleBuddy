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
          
            InputManager = playerController.InputManager;
        }
        protected PlayerController Player { get; private set; }
        protected readonly PlayerStats PlayerStats;
        protected readonly Rigidbody2D Rb;
        protected readonly InputManager InputManager;

        public abstract void EnterState();
        public abstract void ExitState();
    
        public virtual void UpdateState(){}
        public virtual void FixedUpdateState(){}
    
        public virtual void HandleMovement(Vector2 direction) { }
        public virtual void HandleDash() { }
        public virtual void HandleAttack() { }
    }
}
