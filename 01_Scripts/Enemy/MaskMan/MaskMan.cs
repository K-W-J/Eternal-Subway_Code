using System.Collections;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.MaskMan
{
    public class MaskMan : Enemy
    {
        public MaskManStatsSO MaskManStatsSO => BaseEnemyStatsSo as MaskManStatsSO;
        
        [SerializeField] private ParticleHandler _particleHandler;

        private void Start()
        {
            SoundManager.Instance.PlaySFX("Noise2", transform, true);
        }

        protected override void Death()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            
            Collider.enabled = false;
            
            _particleHandler.PlayParticle();
            IsDeath = true;
            
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(_particleHandler.ParticleLifetime());
            
            Destroy(gameObject);
        }
    }
}