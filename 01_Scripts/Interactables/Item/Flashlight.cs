using UnityEngine;

namespace _01_Scripts.Interactables.Item
{
    public class Flashlight : PickUpable.PickUpable, IItem
    {
        public bool IsDisposable { get; set; }
        public bool IsRightNow { get; set; }
        public Player.Player Player { get; set; }
        
        [SerializeField] private int rotationSmooth;
        
        [SerializeField] private Light spotLight;

        private Quaternion _targetWorldRotation;

        private bool _isOn;

        private void Start()
        {
            _targetWorldRotation = transform.rotation;
        }

        private void Update()
        {
            if(transform.parent == null || transform.parent.parent == null)
            {
                _targetWorldRotation = Quaternion.identity;
                return;
            }
            
            Quaternion targetRotation = transform.parent.parent.rotation * Quaternion.Euler(0, 180, 0);

            _targetWorldRotation = Quaternion.Slerp(
                _targetWorldRotation,
                targetRotation,
                rotationSmooth * Time.deltaTime
            );

            transform.rotation = _targetWorldRotation;

        }

        public void UseItem()
        {
            if (_isOn)
            {
                _isOn = false;
                SoundManager.Instance.PlaySFX("FlashLight", transform);
                spotLight.enabled = false;
            }
            else
            {
                _isOn = true;
                SoundManager.Instance.PlaySFX("FlashLight", transform);
                spotLight.enabled = true;
            }
        }

        public void SecondaryUseItem()
        {
            
        }
    }
}