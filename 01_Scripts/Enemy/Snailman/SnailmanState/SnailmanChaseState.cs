using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.Snailman.FSM
{
    public class SnailmanChaseState : EnemyState<Snailman>
    {
        private SnailmanStatsSO _snailmanStats;
        
        private bool _isWaitTime = true;
        private bool _isAttackWaitTime = true;

        public SnailmanChaseState(Snailman entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            _snailmanStats = entity.SnailmanStatsSo;
        }

        public override void Enter()
        {
            
        }
        
        public override void Exit()
        {

        }
        
        public override void StateUpdate()
        {
            m_entity.EnemyMovement.OnChase();
            
            if (m_entity.AttackChecker.AttackCheck())
            {
                Vector3 backForce = m_entity.transform.forward * 10;
                Vector3 upForce = Vector3.up * 4;

                Player.Player player = m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>();
                
                player.PlayerMovement.SetVelocityZero();
                player.PlayerMovement.SetForce(upForce + backForce);
                    
                if(_isAttackWaitTime)
                    m_entity.StartCoroutine(WaitStopAttackTime());
            }
            
            Condition();
        }

        protected override void Condition()
        {
            if (m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>() != null)
            {
                if (!m_entity.TargetCheckerRay.IsPlayerInDetectRange)
                {
                    if(_isWaitTime)
                        m_entity.StartCoroutine(WaitStopChaseTime());
                }
            }
            else if (!m_entity.EnemyMovement.IsMoveing() && m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>() == null)
            {
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Patrol);
            }
        }
        
        private IEnumerator WaitStopAttackTime()
        {
            _isAttackWaitTime = false;
                
            Player.Player player = m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>();
            player.Health.TakeDamage(m_entity.BaseEnemyStatsSo.AttackPower);

            yield return new WaitForSeconds(m_entity.BaseEnemyStatsSo.AttackDelay);
            
            _isAttackWaitTime = true;
        }
        
                            
        private IEnumerator WaitStopChaseTime()
        {
            _isWaitTime = false;
            
            float remainingTime = m_entity.BaseEnemyStatsSo.ChaseTime;
    
            while (remainingTime > 0)
            {
                if (m_entity.TargetCheckerRay.IsPlayerInDetectRange)
                {
                    _isWaitTime = true;
                    yield break;
                }

                remainingTime -= Time.deltaTime;
                yield return null;
            }
            
            _isWaitTime = true;
            m_entity.EnemyMovement.Target = null;
            m_entity.EntityStateMachine.ChangeState(EntityStateType.Patrol); 
        }
    }
}