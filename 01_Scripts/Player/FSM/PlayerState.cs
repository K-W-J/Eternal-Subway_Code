using _01_Scripts.Entity.EntityState;

namespace _01_Scripts.Player.FSM
{
    public class PlayerState : EntityState
    {
        protected Player m_entity;
        public PlayerState(Entity.Entity entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            m_entity = entity as Player;
        }
    }
}