using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class TargetCheckerRay : MonoBehaviour
    {
        public bool IsPlayerInDetectRange { get; set; }

        [SerializeField] private Enemy _agent;
        
        private void Update()
        {
            TargetCheck();
        }

        public void TargetCheck()
        {
            for (int i = 0; i < 9; i++)
            {
                Ray ray = new Ray(transform.position, Quaternion.Euler(0, -180 + i * 20, 0) * transform.forward);
                
                if (Physics.Raycast(ray, out RaycastHit hit, _agent.BaseEnemyStatsSo.DetectRange))
                {
                    if (hit.transform.GetComponentInParent<Player.Player>() != null)
                    {
                        _agent.EnemyMovement.Target = hit.transform.gameObject;
                        IsPlayerInDetectRange = true;
                        return;
                    }
                }
            }

            IsPlayerInDetectRange = false;
        }

        private void OnDrawGizmos()
        {
            if(_agent.BaseEnemyStatsSo == null) return;
            
            for (int i = 0; i < 9; i++)
            {
                Ray ray = new Ray(transform.position, Quaternion.Euler(0, -180 + i * 20, 0) * transform.forward);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(ray.origin, ray.direction * _agent.BaseEnemyStatsSo.DetectRange);
            }
        }
    }
}