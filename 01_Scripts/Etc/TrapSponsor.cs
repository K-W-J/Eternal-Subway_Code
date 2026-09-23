using System.Collections.Generic;
using _01_Scripts.Interactables.Trap;
using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class TrapSponsor : MonoBehaviour
    {
        public List<GameObject> Traps {get; set;} = new List<GameObject>();
        
        [SerializeField] private Clown.Clown _agent;
        
        [SerializeField] private GameObject _trapPrefab;
        [SerializeField] private Transform _trapSpwanPoint;
        
        [SerializeField] private int _trapCount;
        
        
        public bool SpawnTrapCountCheck()
        {
            if(_trapCount > Traps.Count)
                return true;
            
            return false;
        }
        
        public void SpawnTrap()
        {
            GameObject trap = Instantiate(_trapPrefab, _trapSpwanPoint.position,  transform.rotation);
            trap.GetComponent<Trap>().Agent = _agent;
            Traps.Add(trap);
        }
        
        private void OnDestroy()
        {
            foreach (var trap in Traps)
            {
                Destroy(trap);
            }
        }
    }
}