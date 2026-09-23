using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class AttackChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsPlayer;
        
        [SerializeField] private Transform _attackPos;
        [SerializeField] private Transform _longRangeAttackPos;
        
        [SerializeField] private Enemy _agent;

        private LongRangeEnemyStatsSO _longRangeEnemyStatsSO;
        
        private void Awake()
        {
            if(_longRangeAttackPos != null)
                _longRangeEnemyStatsSO = _agent.BaseEnemyStatsSo as LongRangeEnemyStatsSO;
        }
        
        public bool AttackCheck(float additionalAttackRange = 0)
        {
            return Physics.CheckSphere(_attackPos.position, _agent.BaseEnemyStatsSo.AttackRange + additionalAttackRange, _whatIsPlayer);
        }

        public bool LongRangeAttackCheck()
        {
            return Physics.CheckSphere(_attackPos.position, _longRangeEnemyStatsSO.LongRangeAttackSo.LongRangeAttackRange, _whatIsPlayer);
        }
    
        private void OnDrawGizmos()
        {
            if(_agent.BaseEnemyStatsSo == null) return;
            
            Gizmos.DrawWireSphere(_attackPos.position, _agent.BaseEnemyStatsSo.AttackRange);

            if (_longRangeAttackPos != null && _longRangeEnemyStatsSO != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_attackPos.position, _longRangeEnemyStatsSO.LongRangeAttackSo.LongRangeAttackRange);
            }
        }
    }
}
