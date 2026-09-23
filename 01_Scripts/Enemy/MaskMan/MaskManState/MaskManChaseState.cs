using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.MaskMan.FSM
{
    public class MaskManChaseState : EnemyState<MaskMan>
    {
        private MaskManStatsSO _maskmanStats;
        
        private bool _isWaitTime = true;

        public MaskManChaseState(MaskMan entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            _maskmanStats = entity.MaskManStatsSO;
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

            Condition();
        }

        protected override void Condition()
        {
            if (m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>() != null)
            {
                if (m_entity.AttackChecker.AttackCheck())
                {
                    m_entity.EntityStateMachine.ChangeState(EntityStateType.Attack);
                }
                else if (!m_entity.TargetCheckerRay.IsPlayerInDetectRange)
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