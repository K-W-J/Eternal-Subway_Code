using System.Collections;
using UnityEngine;

namespace _01_Scripts
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private Transform _particlePos;
        
        private ParticleSystem _particleSystem;
        
        public void PlayParticle()
        {
            GameObject particleSystem = Instantiate(_particlePrefab.gameObject, _particlePos);
            _particleSystem = particleSystem.GetComponent<ParticleSystem>();
            _particleSystem.Play();
        }

        public void StopParticle() => _particleSystem.Stop();
        public float ParticleLifetime() => _particleSystem.startLifetime;
        
        public void ParticleDestroy() => StartCoroutine(DeathCoroutine());

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(_particleSystem.startLifetime);
                
            Destroy(gameObject);
        }
    }
}