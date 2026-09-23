using UnityEngine;

namespace _01_Scripts.Interactables.Catchable
{
    public class Catchable : Interactable
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Interact(Vector3 target)
        {
            _rigidbody.AddForce((target - transform.position).normalized * 40 * Time.deltaTime, ForceMode.Impulse);
            _rigidbody.linearVelocity *= 0.99f;
        }
    }
}