using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private float shakeDuration = 0.8f;
        [SerializeField] private bool isShake = false;
        
        private CinemachineImpulseSource _impulseSource;
        
        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void Start()
        {
            if(isShake)
                _impulseSource.GenerateImpulse(shakeDuration);
        }

        public void CameraGunShaking()
        {
            RandomShake(0.01f);
            _impulseSource.DefaultVelocity += new Vector3(0, 0, -0.1f);
            _impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = 0.2f;
            _impulseSource.GenerateImpulse(shakeDuration);
        }
        
        public void CameraHitShaking()
        {
            RandomShake2D(0.5f);
            _impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = 0.5f;
            _impulseSource.GenerateImpulse(shakeDuration);
        }

        private void RandomShake(float range)
        {
            float randX = Random.Range(-range, range);
            float randY = Random.Range(-range, range);
            float randZ = Random.Range(-range, range);
            
            _impulseSource.DefaultVelocity = new Vector3(randX, randY, randZ);
        }
        
        private void RandomShake2D(float range)
        {
            float randX = Random.Range(-range, range);
            float randY = Random.Range(-range, range);
            
            _impulseSource.DefaultVelocity = new Vector2(randX, randY);
        }
    }
}
