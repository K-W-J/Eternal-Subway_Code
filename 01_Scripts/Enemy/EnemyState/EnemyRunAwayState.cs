
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Enemy.EnemyState
{
    public class EnemyRunAwayState : EnemyState<Enemy>
    {
        public EnemyRunAwayState(Enemy entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
        }
    }
}