using System.Collections;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.Snailman
{
    public class Snailman : Enemy
    {
        public SnailmanStatsSO SnailmanStatsSo => BaseEnemyStatsSo as SnailmanStatsSO;
        
        [SerializeField] private ParticleHandler _particleHandler;

        protected override void OnEnable()
        {
            base.OnEnable();
            EntityAnimation.OnAnimationEnd += StepSound;
        }

        private void StepSound()
        {
            int rand = SoundManager.Instance.GetRandomSFX(1, 3);
            SoundManager.Instance.PlaySFX("HeavyFootStep" + rand, transform, 10, 5);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EntityAnimation.OnAnimationEnd -= StepSound;
        }

        protected override void Death()
        {
            transform.GetChild(0).gameObject.SetActive(false);

            _particleHandler.PlayParticle();
            IsDeath = true;
            
            Collider.enabled = false;
            
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(_particleHandler.ParticleLifetime());
            
            Destroy(gameObject);
        }
    }
}