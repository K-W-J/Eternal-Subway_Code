using UnityEngine;

namespace _01_Scripts.Interactables.Subway
{
    public class SubwayChecker : MonoBehaviour
    {
        public bool IsPlayerInSubway { get; private set; }
        
        [SerializeField] private LayerMask _whatIsPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if((_whatIsPlayer.value & (1 << other.gameObject.layer)) != 0)
            {
                IsPlayerInSubway = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if((_whatIsPlayer.value & (1 << other.gameObject.layer)) != 0)
            {
                IsPlayerInSubway = false;
            }
        }
    }
}