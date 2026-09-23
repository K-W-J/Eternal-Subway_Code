using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Enemy.EnemyState
{
    public class EnemyPatrolState : EnemyState<Enemy>
    {
        public EnemyPatrolState(Enemy entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        protected virtual void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd += PlaySound;
        }

        protected virtual void PlaySound()
        {
            if(m_entity.EntityStateMachine.CurrentState != EntityStateType.Patrol) return;
            
            int rand = SoundManager.Instance.GetRandomSFX(1, 4);
            SoundManager.Instance.PlaySFX("FootStep" + rand, m_entity.transform);
        }

        protected virtual void OnDisableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd -= PlaySound;
        }
        public override void Enter()
        {
            OnEnableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
            m_entity.EnemyMovement.PickPatrolPoint();
        }
        
        public override void Exit()
        {
            OnDisableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
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
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Idle);
        }
    }
}