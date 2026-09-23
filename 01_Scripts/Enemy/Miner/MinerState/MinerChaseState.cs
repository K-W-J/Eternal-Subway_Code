using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using UnityEngine;

namespace _01_Scripts.Enemy.Miner.FSM
{
    public class MinerChaseState : EnemyState<Miner>
    {
        private bool _isWaitTime = true;
        private bool _isFireWaitTime = true;

        public MinerChaseState(Miner entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        protected virtual void OnEnableEvent()
        {
            m_entity.EntityAnimation.OnAnimationEnd += PlaySound;
        }

        protected virtual void PlaySound()
        {
            if(m_entity.EntityStateMachine.CurrentState != EntityStateType.Chase) return;
            
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
        }
        
        public override void Exit()
        {
            OnDisableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
            m_entity.EntityAnimation.SetBool(Animator.StringToHash("IDLE"), false); 
        }
        
        public override void StateUpdate()
        {
            m_entity.EnemyMovement.OnChase();

            if (m_entity.EnemyMovement.IsVelocityZero())
            {
                m_entity.EntityAnimation.SetBool(Animator.StringToHash("IDLE"), true);
                m_entity.EntityAnimation.SetBool(m_animationHash, false);
            }
            else
            {
                m_entity.EntityAnimation.SetBool(Animator.StringToHash("IDLE"), false);
                m_entity.EntityAnimation.SetBool(m_animationHash, true);
            }
            
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
                else if (!m_entity.AttackChecker.AttackCheck() && m_entity.AttackChecker.LongRangeAttackCheck())
                {
                    if(_isFireWaitTime)
                        m_entity.StartCoroutine(WaitStopFireAttackTime());
                }
                else if (!m_entity.TargetCheckerRay.IsPlayerInDetectRange)
                {
                    if(_isWaitTime)
                        m_entity.StartCoroutine(WaitStopChaseTime());
                }
            }
            else if (!m_entity.EnemyMovement.IsMoveing() && m_entity.EnemyMovement.Target.GetComponentInParent<Player.Player>() == null)
            {
                m_entity.EnemyMovement.Target = null;
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Idle);
            }
        }
        
        private IEnumerator WaitStopFireAttackTime()
        {
            _isFireWaitTime = false;
            
            float remainingTime = m_entity.MinerStatsSO.LongRangeAttackSo.LongRangeAttackTime;
    
            while (remainingTime > 0)
            {
                if (m_entity.AttackChecker.AttackCheck() || !m_entity.AttackChecker.LongRangeAttackCheck())
                {
                    _isFireWaitTime = true;
                    yield break;
                }

                remainingTime -= Time.deltaTime;
                yield return null;
            }
            
            _isFireWaitTime = true;
            m_entity.EntityStateMachine.ChangeState(EntityStateType.Fire); 
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
            m_entity.EntityStateMachine.ChangeState(EntityStateType.Idle); 
        }
    }
}