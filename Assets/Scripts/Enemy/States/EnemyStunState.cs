using UnityEngine;
using Utilities;

namespace Enemy.States
{
    public class EnemyStunState : EnemyBaseState
    {
        public EnemyStunState(EnemyBase enemy) : base(enemy)
        {
        }
        public override void EnterState()
        {
            Enemy.StunTimer.Reset();
            Enemy.StunTimer.Start();
        }

        public override void ExitState()
        {
           Enemy.IsAttacking = false; 
        }

        public override void UpdateState()
        {
            if (Enemy.StunTimer.IsFinished)
            {
                Enemy.ChangeState(Enemy.ChaseState);
            }
        }
        
        
    }
}