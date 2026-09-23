using UnityEngine;

namespace _01_Scripts.Level
{
    public class MapOverlapChecker : MonoBehaviour
    {
        [field:SerializeField] public GameObject mapRander { get; set; }
        [SerializeField] private GameObject colliderChecker;
        [SerializeField] private LayerMask isMapPart;
        
        private BoxCollider[] _mapColliders;
        private bool _isClipping;

        private void Awake()
        {
            if (colliderChecker != null)
            {
                _mapColliders = colliderChecker.GetComponents<BoxCollider>();
            }
        }

        public bool OverlapCheck()
        {
            if (_mapColliders == null || _mapColliders.Length == 0)
            {
                Debug.LogWarning($"{gameObject.name}에 검사용 BoxCollider가 없습니다.");
                return false;
            }

            if (mapRander != null) mapRander.SetActive(false);
            
            _isClipping = false;

            foreach (BoxCollider boxCollider in _mapColliders)
            {
                if (boxCollider == null) continue;

                Vector3 center = colliderChecker.transform.TransformPoint(boxCollider.center);
                Vector3 halfExtents = boxCollider.size * 0.5f;
                    
                _isClipping = Physics.CheckBox(center, halfExtents, colliderChecker.transform.rotation, isMapPart);

                if (_isClipping)
                    break;
            }
            
            if (!_isClipping && mapRander != null)
            {
                mapRander.SetActive(true);
            }
            
            return _isClipping;
        }
        
        private void OnDrawGizmos()
        {
            if (colliderChecker == null) return;
            
            BoxCollider[] mapCollider = colliderChecker.GetComponents<BoxCollider>();
            
            Gizmos.color = Color.red;

            foreach (var boxCollider in mapCollider)
            {
                if (boxCollider == null) continue;

                Vector3 worldCenter = boxCollider.transform.TransformPoint(boxCollider.center);

                Vector3 worldSize = boxCollider.size * 0.5f;

                Gizmos.matrix = boxCollider.transform.localToWorldMatrix;
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
        }
    }
}