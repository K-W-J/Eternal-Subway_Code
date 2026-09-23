using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class TargetCheckerSphere : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private Transform _checkerPos;
        [SerializeField] private Enemy _agent;

        private void Update()
        {
            TargetCheck();
        }
        private void TargetCheck()
        {
            Collider[] player = Physics.OverlapSphere(_checkerPos.position, _agent.BaseEnemyStatsSo.DetectRange, _whatIsPlayer);
            
            if(player[0] != null)
                _agent.EnemyMovement.Target = player[0].gameObject;
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_checkerPos.position, _agent.BaseEnemyStatsSo.DetectRange);
            Gizmos.color = Color.blue;
        }
    }
}
