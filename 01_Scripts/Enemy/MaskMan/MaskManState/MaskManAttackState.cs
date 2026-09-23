using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.MaskMan.FSM
{
    public class MaskManAttackState : EnemyState<MaskMan>
    {
        private MaskManStatsSO _maskmanStats;
        
        private bool _isWaitTime = true;

        public MaskManAttackState(MaskMan entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {
            _maskmanStats = entity.MaskManStatsSO;
        }

        public override void Enter()
        {
            m_entity.EnemyMovement.OnStop();
        }
        
        public override void Exit()
        {
        }
        
        public override void StateUpdate()
        {
            if (_isWaitTime)
                m_entity.StartCoroutine(WaitStopAttackTime());
            
            Condition();
        }

        protected override void Condition()
        {
            if (!m_entity.AttackChecker.AttackCheck())
            {
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Chase);
            }
        }
        
        private IEnumerator WaitStopAttackTime()
        {
            _isWaitTime = false;

            yield return new WaitForSeconds(m_entity.BaseEnemyStatsSo.AttackDelay);
            
            Player.Player player = m_entity.EnemyMovement.Target.GetComponent<Player.Player>();
            player.Health.TakeDamage(m_entity.BaseEnemyStatsSo.AttackPower);
            
            
            _isWaitTime = true;

        }
    }
}