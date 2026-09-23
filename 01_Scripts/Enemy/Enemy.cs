using System.Collections;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class Enemy : Entity.Entity
    {
        [field:SerializeField] public TargetCheckerRay TargetCheckerRay { get; private set; }
        [field:SerializeField] public EnemyMovement EnemyMovement { get; private set; }
        
        [field:SerializeField] public AttackChecker AttackChecker { get; private set; }
        [field:SerializeField] public BaseEnemyStatsSO BaseEnemyStatsSo { get; private set; }
        
        [field:SerializeField] public Collider Collider { get; set; }
        
        private Rigidbody[] _rigidbodies;

        protected virtual void Awake()
        {
            Health.SetMaxHealth(BaseEnemyStatsSo.MaxHealth);;
            
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }
        protected override void Death()
        {
            base.Death();
            
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
