
using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Player.FSM
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity.Entity entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }
        
        public override void Enter()
        {
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
        }
        
        public override void Exit()
        {
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
            else if(m_entity.PlayerMovement.Velocity.magnitude != 0)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Walk);
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