using UnityEngine;

namespace Enemy.States
{
    public class EnemyChaseState : EnemyBaseState
    {
        public EnemyChaseState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            Enemy.SetSpriteNormal();
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            if (Detector.CanAttack())
            {
                Enemy.ChangeState(Enemy.AttackState);
            }
            else
            {
                if (!Enemy.IsHit)
                {
                    Enemy.FollowPlayer();
                }
            }
        }
    }
}