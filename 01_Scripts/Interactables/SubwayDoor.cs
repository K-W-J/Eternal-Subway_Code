using Unity.AI.Navigation;
using UnityEngine;

namespace _01_Scripts.Interactables
{
    public class SubwayDoor : Door
    {
        [SerializeField] private NavMeshLink _navMeshLink;
        
        public override void Interact()
        {
            base.Interact();
            
            if (_isOpened)
            {
                _navMeshLink.enabled = true;
            }
            else
            {
                _navMeshLink.enabled = false;
            }
        }
    }
}