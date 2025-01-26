namespace Enemy.States
{
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            Animator.PlayChargeAnimation();
        }

        public override void ExitState()
        {
        
        }
    }
}
