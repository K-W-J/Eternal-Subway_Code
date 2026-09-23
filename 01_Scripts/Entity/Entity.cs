using _01_Scripts.Entity.EntityState;
using UnityEngine;

namespace _01_Scripts.Entity
{
    public class Entity : MonoBehaviour
    {
        [field: SerializeField] public EntityStateMachine EntityStateMachine { get; private set; }

        [field: SerializeField] public EntityAnimation EntityAnimation { get; private set; }
        
        public bool IsDeath { get; set; }
        public Health Health { get; private set; } = new Health();

        protected virtual void OnEnable()
        {
            Health.OnDeath += Death;
            Health.OnTakeDamage += HitSound;
        }

        protected virtual void OnDisable()
        {
            Health.OnDeath -= Death;
            Health.OnTakeDamage -= HitSound;
        }

        protected virtual void Death()
        {
            IsDeath = true;
        }
        
        protected virtual void HitSound()
        {
            int rand = SoundManager.Instance.GetRandomSFX(1, 3);
            SoundManager.Instance.PlaySFX("BodyPunch" + rand, transform, 10, 5);
        }
    }
}