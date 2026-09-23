using System;
using System.Collections;
using UnityEngine;

namespace _01_Scripts.NPC
{
    public class Npc : Entity.Entity
    {
        public Action OnDeathNpc;
        [field:SerializeField] public Collider Collider { get; set; }
        
        private Rigidbody[] _rigidbodies;

        protected virtual void Awake()
        {
            Health.SetMaxHealth(50);
            
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }
        
        protected override void Death()
        {
            base.Death();
            
            SoundManager.Instance.PlaySFX("MaleScream", transform);
            
            OnDeathNpc?.Invoke();
            
            foreach (var rigidbodie in _rigidbodies)
            {
                rigidbodie.isKinematic = false;
            }
            
            Collider.enabled = false;
            
            EntityAnimation.SetAnimationActive(false);

            StartCoroutine(DeathCoroutine());
        }
        
        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(5f);
            
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}