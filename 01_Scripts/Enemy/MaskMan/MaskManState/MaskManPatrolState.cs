using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using _01_Scripts.SO.Enitity.Enemy;

namespace _01_Scripts.Enemy.MaskMan.FSM
{
    public class MaskManPatrolState : EnemyState<MaskMan>
    {
        private MaskManStatsSO _maskmanStats;
        public MaskManPatrolState(MaskMan entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            _maskmanStats = entity.MaskManStatsSO;
        }

        public override void Enter()
        {
            m_entity.EnemyMovement.PickPatrolPoint();
        }
        
        public override void Exit()
        {

        }
        
        public override void StateUpdate()
        {
            m_entity.EnemyMovement.OnPatrol();
            
            Condition();
        }

        protected override void Condition()
        {
            if(m_entity.EnemyMovement.Target != null)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Chase);
            else if(!m_entity.EnemyMovement.IsMoveing())
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Patrol);
        }
    }
}