using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using UnityEngine;

namespace _01_Scripts.Enemy.Miner.FSM
{
    public class MinerLongRangeAttackState : EnemyState<Miner>
    {
        private bool _isAttack = true;

        public MinerLongRangeAttackState(Miner entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        private void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnFireAttack += Attack;
            m_entity.EntityAnimation.IsAttackAnimation += IsAttack;
            m_entity.EntityAnimation.IsAttackAnimation += IsAttack;
        }

        private void IsAttack(bool isAttack) => _isAttack = isAttack;

        private void OnDisableEvent()
        {
            m_entity.EntityAnimation.OnFireAttack -= Attack;
            m_entity.EntityAnimation.IsAttackAnimation -= IsAttack;
            m_entity.EntityAnimation.IsAttackAnimation -= IsAttack;
        }
        
        private void Attack()
        {
            SoundManager.Instance.PlaySFX("Swing1", m_entity.transform);
            
            Player.Player player = m_entity.EnemyMovement.Target.GetComponent<Player.Player>();
            
            GameObject bullet = Object.Instantiate(m_entity.MinerStatsSO.Bullet, 
                m_entity.FirePos);
            
            bullet.transform.SetParent(null);
            bullet.transform.LookAt(player.transform.position);
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
            _isAttack = true;
        }
        
        public override void StateUpdate()
        {
            if (m_entity.EnemyMovement.Target != null)
            {
                Vector3 targetPosition = m_entity.EnemyMovement.Target.transform.position;
                Vector3 direction = targetPosition - m_entity.transform.position;
                
                direction.y = 0;
                
                m_entity.transform.LookAt(m_entity.transform.position + direction);
            }
            
            Condition();
        }

        protected override void Condition()
        {
            if (!_isAttack)
            {
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Chase);
            }
            else if (m_entity.AttackChecker.AttackCheck() && !_isAttack)
            {
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Attack);
            }
        }
    }
}