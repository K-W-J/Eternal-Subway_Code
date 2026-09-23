using UnityEngine;

namespace _01_Scripts.Interactables.Subway
{
    public class HydraulicPress : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("OPEN");
        
        [SerializeField] private Animator _animation;
        
        [SerializeField] private bool _isOpened;
        
        public void AnimationEnd()
        {
            _isOpened = true;
            _animation.SetBool(Open, true);
        }
        
        public void Interact()
        {
            if (_isOpened)
            {
                SoundManager.Instance.PlaySFX("HydraulicPress", transform);
                
                _isOpened = false;
                _animation.SetBool(Open, false);
            }
        }
    }
}