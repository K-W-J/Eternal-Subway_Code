using UnityEngine.Events;

namespace _01_Scripts.Interactables
{
    public class Butten : Interactable
    {
        public UnityEvent OnButtonClick;
        public override void Interact()
        {
            OnButtonClick.Invoke();
            SoundManager.Instance.PlaySFX("FlashLight", transform);
        }

    }
}