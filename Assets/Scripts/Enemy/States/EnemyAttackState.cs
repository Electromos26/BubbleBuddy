using UnityEngine;

namespace Enemy.States
{
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            Enemy.ChargeUpAttack();
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            if (Enemy.ChargeUpFinished && !Enemy.IsAttacking)
            {
                Enemy.AttackPlayer();
            } 
        }
    }
}