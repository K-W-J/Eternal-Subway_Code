using UnityEngine;
using UnityEngine.AI;

namespace _01_Scripts.Interactables
{
    public class Door : Interactable
    {
        private readonly int _open = Animator.StringToHash("OPEN");
        
        [SerializeField] protected Animator _animation;
        [SerializeField] protected NavMeshObstacle _navMeshObstacle;
        
        [SerializeField] protected bool _isOpened;

        private void Awake()
        {
            _navMeshObstacle.enabled = false;
        }

        public override void Interact()
        {
            if (_isOpened)
            {
                SoundManager.Instance.PlaySFX("MetalDoor", transform);
                _isOpened = false;
                _navMeshObstacle.enabled = true;
                _animation.SetBool(_open, false);
            }
            else
            {
                SoundManager.Instance.PlaySFX("MetalDoor", transform);
                _isOpened = true;
                _navMeshObstacle.enabled = false;
                _animation.SetBool(_open, true);
            }
        }
    }
}