using System;
using _01_Scripts.Level;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Enemy.Looker
{
    public class Looker : Enemy
    {
        [field:SerializeField] public AutoRangedAttack AutoRangedAttack { get; set; }
        [field:SerializeField] public LookCounter LookCounter { get; private set; }
        public LookerStatsSO LookerStatsSo => BaseEnemyStatsSo as LookerStatsSO;
        
        public AudioSource AudioSource { get; set; }
    
        protected override void Awake()
        {
            base.Awake();
            
            EnemyMovement.Target = GameManager.Instance.Player.gameObject;
            EntityAnimation.LookTarget = GameManager.Instance.Player.transform;
            
            EntityAnimation.gameObject.SetActive(false);
            Collider.gameObject.SetActive(false);
        }

        private void Start()
        {
            SoundManager.Instance.PlaySFX("Noise3", transform, true);

            AudioSource = GetComponentInChildren<AudioSource>();
            
            AudioSource.enabled = false;
        }

        public Vector3 LookerRanodmSpawn() => GameManager.Instance.LookerSpawnPoints[Random.Range(0, GameManager.Instance.LookerSpawnPoints.Count)].position;
    }
}