namespace Enemy.States
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            if (Detector.HasDetected)
            {
                EnemyBase.ChangeState(EnemyBase.ChaseState);
            }
        }
    }
}