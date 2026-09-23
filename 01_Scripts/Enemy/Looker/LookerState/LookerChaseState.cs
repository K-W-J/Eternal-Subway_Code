using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using _01_Scripts.Level;
using UnityEngine;

namespace _01_Scripts.Enemy.Looker.FSM
{
    public class LookerChaseState : EnemyState<Looker>
    {
        private bool _isPlayerInSubway;
        private bool _isWaitTime;
        private bool _isLook;
        private bool _isChase = true;

        public LookerChaseState(Looker entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            
        }

        private void OnEnableEvent()
        {
            m_entity.LookCounter.OnLookCount += LookCount;
        }

        private void OnDisableEvent()
        {
            m_entity.LookCounter.OnLookCount -= LookCount;

        }

        private void LookCount()
        {
            if (m_entity.LookCounter.LookCount == 4)
            {
                _isLook = true;
                _isChase = false;
            
                ResetChase(true);
                m_entity.StopCoroutine(SetActivDaley());
            
                m_entity.transform.position = m_entity.LookerRanodmSpawn();
            }
            else
            {
                ResetChase(false);
            }
        }
        
        public override void Enter()
        {
            OnEnableEvent();
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
        }
        
        public override void Exit()
        {
            OnDisableEvent();
            m_entity.EntityAnimation.gameObject.SetActive(true);
            m_entity.EntityAnimation.SetBool(m_animationHash, false);
        }
        
        public override void StateUpdate()
        {
            if(!_isWaitTime)
                m_entity.StartCoroutine(StayDaley());

            if (!_isPlayerInSubway && _isLook)
                _isPlayerInSubway = GameManager.Instance.SubwayChecker.IsPlayerInSubway;
            
            Condition();
        }

        protected override void Condition()
        {
            if(m_entity.AttackChecker.AttackCheck() && _isLook && _isPlayerInSubway)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Attack);
        }
        
        private IEnumerator StayDaley()
        {
            _isWaitTime = true;

            float randomXPos = 0;
            float randomZPos = 0;
                
            float remainingTime = 0;
            
            if (!_isLook)
            {
                float _randomChasePos = m_entity.LookerStatsSo.RandomChasePos;
                
                randomXPos = Random.Range(-_randomChasePos, _randomChasePos);
                randomZPos = Random.Range(-_randomChasePos, _randomChasePos); 
                
                float _minRemainingChaseTime = m_entity.LookerStatsSo.minRemainingChaseTime;
                float _maxRemainingChaseTime = m_entity.LookerStatsSo.maxRemainingChaseTime;
                
                remainingTime = Random.Range(_minRemainingChaseTime,_maxRemainingChaseTime);
            }
            else if(_isLook && _isPlayerInSubway)
            {
                float _randomAttackPos = m_entity.LookerStatsSo.RandomAttackPos;
                
                randomXPos = Random.Range(-_randomAttackPos, _randomAttackPos);
                randomZPos = Random.Range(-_randomAttackPos, _randomAttackPos); 
                
                float _minRemainingAttackTime = m_entity.LookerStatsSo.minRemainingAttackTime;
                float _maxRemainingAttackTime = m_entity.LookerStatsSo.maxRemainingAttackTime;
                
                remainingTime = Random.Range(_minRemainingAttackTime, _maxRemainingAttackTime);
            }

            while (remainingTime >= 0)
            {
                if (!_isChase)
                {
                    if (_isLook && !_isPlayerInSubway)
                    {
                        _isWaitTime = false;
                        yield break;
                    }
                }
                
                remainingTime -= Time.deltaTime;
                    
                yield return null;
            }
            
            if (!_isLook)
            {
                m_entity.StartCoroutine(SetActivDaley());
            }
            
            Vector3 playerPosition = GameManager.Instance.Player.transform.position 
                + new Vector3(randomXPos, 0, randomZPos);

            m_entity.EnemyMovement.OnWarp(playerPosition);
            m_entity.transform.position = playerPosition;
            
            if(_isLook)
                _isWaitTime = false;
        }

        private IEnumerator SetActivDaley()
        {
            ResetChase(true);
            
            yield return new WaitForSeconds(7f);

            if (!_isChase)
            {
                _isWaitTime = false;
                yield break;
            }

            ResetChase(false);
            
            _isWaitTime = false;
        }

        public void ResetChase(bool isLook)
        {
            
            m_entity.AudioSource.enabled = isLook;
            m_entity.Collider.gameObject.SetActive(isLook);
            m_entity.EntityAnimation.gameObject.SetActive(isLook);
        }
    }
}