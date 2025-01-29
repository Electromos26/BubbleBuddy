using Managers;

namespace Enemy.States
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyBase enemy) : base(enemy)
        {
        }
        
        public override void EnterState()
        {
            Enemy.PlayDeathAnimation();
            Enemy.DropBubble();
        }

        public override void ExitState()
        {
            
        }
        
        
    }
}