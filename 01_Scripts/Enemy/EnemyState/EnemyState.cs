using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Enemy.EnemyState
{
    public class EnemyState<T> : EntityState where T : Enemy
    {
        protected T m_entity;
        protected T Entity => m_entity;

        public EnemyState(Enemy entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            m_entity = entity as T;
            
        }
    }
}