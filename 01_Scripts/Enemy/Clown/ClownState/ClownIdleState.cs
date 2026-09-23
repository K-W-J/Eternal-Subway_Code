using System.Collections;
using _01_Scripts.Enemy.EnemyState;
using _01_Scripts.Entity.EntityState;
using UnityEngine;

namespace _01_Scripts.Enemy.Clown.FSM
{
    public class ClownIdleState : EnemyState<Clown>
    {
        public ClownIdleState(Clown entity, EntityStateType stateType, int animationHash) : base(entity, stateType, animationHash)
        {

        }

        public override void Enter()
        {
            m_entity.EntityAnimation.SetBool(m_animationHash, true);
            m_entity.EnemyMovement.Target = null;
            m_entity.EntityAnimation.LookTarget = null;
            m_entity.EnemyMovement.OnStop();
            m_entity.StartCoroutine(StayDaley());
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
            if(m_entity.EnemyMovement.Target != null)
                m_entity.EntityStateMachine.ChangeState(EntityStateType.Chase);
        }

        private IEnumerator StayDaley()
        {
            yield return new WaitForSeconds(Random.Range(m_entity.ClownStatsSO.TrapSo.TarpDelayMin, m_entity.ClownStatsSO.TrapSo.TarpDelayMin));
            
            if (m_entity.TrapSponsor.SpawnTrapCountCheck() && Random.Range(0, 2) == 1)
                m_entity.TrapSponsor.SpawnTrap();
            
            m_entity.EntityStateMachine.ChangeState(EntityStateType.Patrol);
            
        }
    }
}