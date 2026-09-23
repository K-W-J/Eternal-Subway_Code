using UnityEngine;

namespace _01_Scripts.Enemy.Miner
{
    public class PickaxeBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _attackPower;
        
        private bool _isStop;
        private void Start()
        {
            Destroy(gameObject, 10);
        }

        private void Update()
        {
            if(_isStop) return;
            
            transform.position += transform.forward * (_speed * Time.deltaTime);
            transform.GetChild(0).rotation *= Quaternion.Euler(2 * 360 * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponentInParent<Player.Player>();
            if (player != null)
            {
                player.Health.TakeDamage(_attackPower);
                Destroy(gameObject);
            }
            
            Destroy(gameObject, 5);
        }
    }
}