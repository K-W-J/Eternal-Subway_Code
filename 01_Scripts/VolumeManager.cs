using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _01_Scripts
{
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _volume;
        
        [SerializeField] private float _fadeSpeed;
        [SerializeField] private float _vignetteSpeed;
        public ColorAdjustments ColorAdjustments { get; set; }
        private float _colorAdjustmentsValue;

        public Vignette Vignette { get; set; }
        private float _vignetteValue;

        public bool IsFadeRuning { get; private set; }
        
        private void Awake()
        {
            if (_volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                ColorAdjustments = colorAdjustments;
                _colorAdjustmentsValue = ColorAdjustments.postExposure.value;
                ColorAdjustments.postExposure.value = -20;
            }

            if (_volume.profile.TryGet(out Vignette vignette))
            {
                Vignette = vignette;
                _vignetteValue = Vignette.intensity.value;
            }
            
        }

        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        public void VignetteOutIn(bool isTakeDamage)
        {
            if (isTakeDamage)
            {
                StartCoroutine(BaseVignette());
            }
            else
            {
                StartCoroutine(DamageVignette());
            }
        }
        
        public IEnumerator FadeOut()
        {
            IsFadeRuning = true;
            
            while (ColorAdjustments.postExposure.value > -10)
            {
                ColorAdjustments.postExposure.value -= _fadeSpeed * Time.deltaTime;
                yield return null;
            }

            ColorAdjustments.postExposure.value -= 0.01f;

            FadeIn();
        }
        
        public IEnumerator FadeIn()
        {
            while (ColorAdjustments.postExposure.value < _colorAdjustmentsValue)
            {
                ColorAdjustments.postExposure.value += _fadeSpeed * Time.deltaTime;
                yield return null;
            }

            ColorAdjustments.postExposure.value = _colorAdjustmentsValue;

            IsFadeRuning = false;
        }
        
        
        
        public IEnumerator BaseVignette()
        {
            while (Vignette.intensity.value > _vignetteValue)
            {
                Vignette.intensity.value -= _vignetteSpeed * Time.deltaTime;
                yield return null;
            }

            Vignette.color.value = Color.black;
            Vignette.intensity.value = _vignetteValue;
        }
        
        public IEnumerator DamageVignette()
        {
            Vignette.color.value = Color.red;
            
            while (Vignette.intensity.value < 0.5)
            {
                Vignette.intensity.value += _vignetteSpeed * Time.deltaTime;
                yield return null;
            }

            Vignette.intensity.value = 0.5f;
            
        }
        
        public IEnumerator HealVignette()
        {
            Vignette.color.value = Color.green;
            
            while (Vignette.intensity.value < 0.5)
            {
                Vignette.intensity.value += 0.5f * Time.deltaTime;
                yield return null;
            }

            Vignette.intensity.value = 0.5f;
            
        }
    }
}