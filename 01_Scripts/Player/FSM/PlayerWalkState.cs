
using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Player.FSM
{
    public class PlayerWalkState : PlayerState
    {
        public PlayerWalkState(Entity.Entity entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }
        
        private void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd += PlaySound;
        }

        private void PlaySound()
        {
            if(m_entity.EntityStateMachine.CurrentState != EntityStateType.Walk) return;
            
            int rand = SoundManager.Instance.GetRandomSFX(1, 4);
            SoundManager.Instance.PlaySFX("FootStep" + rand, m_entity.transform);
        }

        private void OnDisableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd -= PlaySound;
        }
        public override void Enter()
        {
            OnEnableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
        }
        
        public override void Exit()
        {
            OnDisableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
        }
        
        public override void StateUpdate()
        {
            Condition();
        }

        protected override void Condition()
        {
            if(m_entity.PlayerMovement.Velocity.magnitude != 0 && m_entity.PlayerMovement.IsRuning)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Run);
            else if(m_entity.PlayerMovement.Velocity.magnitude == 0)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Idle);
            else if (!m_entity.GroundChecker.GroundCheck())
            {
                if(m_entity.PlayerMovement.IsJumping)
                    m_entity.EntityStateMachine.ChangeState(EntityStateType.Jump);
                else if(m_entity.PlayerMovement.IsFalling)
                    m_entity.EntityStateMachine.ChangeState(EntityStateType.Fall);
            }
        }
    }
}