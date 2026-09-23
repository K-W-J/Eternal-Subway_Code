using UnityEngine;

namespace _01_Scripts.Interactables
{
    public class Interactable : MonoBehaviour
    {
        [field:SerializeField] public bool IsKeepClicking { get; private set; }
        public virtual void Interact()
        {
            
        }
    }
}