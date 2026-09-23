using UnityEngine;

namespace _01_Scripts.Enemy.Looker
{
    public class LookerSpawnChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsMap;

        public bool CanSpawn { get; private set; }

        private void Update()
        {
            SpawnCheck();
        }

        private void SpawnCheck()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
                
            if (Physics.Raycast(ray, 0.1f, _whatIsMap))
            {
                CanSpawn = true;
            }
            else
            {
                CanSpawn = false;
            }
        }
    }
}