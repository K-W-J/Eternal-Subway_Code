using _01_Scripts.Entity.EntityState;
using UnityEngine;

namespace _01_Scripts.Enemy.EnemyState
{
    public class EnemyAttackState : EnemyState<Enemy>
    {
        private bool _isAttack;

        public EnemyAttackState(Enemy entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        protected void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnAttack += Attack;
            m_entity.EntityAnimation.IsAttackAnimation += IsAttack;
            m_entity.EntityAnimation.IsAttackAnimation += IsAttack;
        }

        protected void OnDisableEvent()
        {
            m_entity.EntityAnimation.OnAttack -= Attack;
            m_entity.EntityAnimation.IsAttackAnimation -= IsAttack;
            m_entity.EntityAnimation.IsAttackAnimation -= IsAttack;
        }

        private void IsAttack(bool isAttack) => _isAttack = isAttack;

        private void Attack()
        {
            if(!m_entity.AttackChecker.AttackCheck(1f)) return;
            
            Player.Player player = m_entity.EnemyMovement.Target.GetComponent<Player.Player>();
            player.Health.TakeDamage(m_entity.BaseEnemyStatsSo.AttackPower);
        }

        public override void Enter()
        {
            OnEnableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
            m_entity.EnemyMovement.OnStop();
        }
        
        public override void Exit()
        {
            OnDisableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
        }
        
        public override void StateUpdate()
        {
            Vector3 targetPosition = m_entity.EnemyMovement.Target.transform.position;
            Vector3 direction = targetPosition - m_entity.transform.position;
            
            direction.y = 0;
            
            m_entity.transform.LookAt(m_entity.transform.position + direction);
            
            Condition();
        }

        protected override void Condition()
        {
            if (!m_entity.AttackChecker.AttackCheck() && !_isAttack)
            {
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Chase);
            }
        }
    }
}