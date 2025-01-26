namespace Enemy.States
{
    public abstract class EnemyBaseState 
    {
        protected EnemyBaseState(EnemyBase enemy)
        {
            EnemyBase = enemy;
            Animator = enemy.Animator;
            Detector = enemy.Detector;
        }

        protected EnemyBase EnemyBase { get; private set; }
        protected EnemyAnimator Animator { get; private set; }
        protected PlayerDetector Detector { get; private set; }
        
        public abstract void EnterState();
        public abstract void ExitState();
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
    
    }
}
