
using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Player.FSM
{
    public class PlayerFallState : PlayerState
    {
        public PlayerFallState(Entity.Entity entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        private void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd += PlaySound;
        }

        private void PlaySound()
        {
            //SoundManager.Instance.PlaySFX("FootStep", m_entity.transform);
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
            
            PlaySound();
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
        }
        
        public override void StateUpdate()
        {
            Condition();
        }

        protected override void Condition()
        {
            if (m_entity.GroundChecker.GroundCheck())
            {
                int rand = SoundManager.Instance.GetRandomSFX(1, 4);
                SoundManager.Instance.PlaySFX("FootStep" + rand, m_entity.transform);
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Idle);
            }
        }
    }
}