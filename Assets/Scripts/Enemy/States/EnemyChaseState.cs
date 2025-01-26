namespace Enemy.States
{
    public class EnemyChaseState: EnemyBaseState
    {
        public EnemyChaseState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            EnemyBase.FollowPlayer();
        }

        public override void ExitState()
        {
   
        }

        public override void UpdateState()
        {
            if (Detector.CanAttack)
            {
                EnemyBase.ChangeState(EnemyBase.AttackState);
            }
        }
     
    }
}