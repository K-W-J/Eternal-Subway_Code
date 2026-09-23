using UnityEngine;

namespace _01_Scripts.Player
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Vector3 _boxSize;

        public bool GroundCheck()
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, _boxSize, Quaternion.identity, _whatIsGround);

            return colliders.Length > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _boxSize);
            
        }
    }
}