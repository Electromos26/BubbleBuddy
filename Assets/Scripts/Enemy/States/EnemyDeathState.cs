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
        }

        public override void ExitState()
        {
            
        }
        
        
    }
}