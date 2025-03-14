namespace Enemy.States
{
    public abstract class EnemyBaseState 
    {
        protected EnemyBaseState(EnemyBase enemy)
        {
            Enemy = enemy;
            Detector = enemy.Detector;
        }

        protected EnemyBase Enemy { get; private set; }
        protected PlayerDetector Detector { get; private set; }
        
        public abstract void EnterState();
        public abstract void ExitState();
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
    
    }
}
