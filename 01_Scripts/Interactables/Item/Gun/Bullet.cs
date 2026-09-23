using UnityEngine;

namespace _01_Scripts.Interactables.Item.Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _attackPower;
        [SerializeField] private float _speed;

        private bool _isTouch;
        
        private void Start()
        {
            Destroy(gameObject, 5);
        }

        private void Update()
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isTouch) return;

            _isTouch = true;
            
            var entity = other.GetComponentInParent<Entity.Entity>();
            if (entity != null)
            {
                entity.Health.TakeDamage(_attackPower);
                Destroy(gameObject);
            }
        }
    }
}