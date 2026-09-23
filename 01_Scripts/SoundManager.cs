using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private GameObject _soundPrefab;
        
        [SerializeField] private AudioClip[] _sfxClips;
        [SerializeField] private AudioClip[] _bgmClips;
        
        private Dictionary<string, AudioClip> _sfxSounds = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioClip> _bgmSounds = new Dictionary<string, AudioClip>();
        
        private Dictionary<string, AudioSource> _bgmTemp = new Dictionary<string, AudioSource>();
        
        private void Awake()
        {
            if (_sfxClips.Length > 0)
            {
                foreach (var sfxClip in _sfxClips)
                {
                    _sfxSounds[sfxClip.name] = sfxClip;
                }
            }

            if (_bgmClips.Length > 0)
            {
                foreach (var bgmClip in _bgmClips)
                {
                    _bgmSounds[bgmClip.name] = bgmClip;
                }
            }
        }

        public void PlayBGM(string bgmName, Transform bgmPos, float volume = 1f, float pitch = 0f)
        {
            if (_bgmTemp.ContainsKey(bgmName))
            {
                _bgmTemp[bgmName].Play();
                return;
            }
            
            if (!_bgmSounds.ContainsKey(bgmName))
            {
                Debug.LogWarning($"Sound {bgmName} not found");
                return;
            }
            
            GameObject playSoundPos = Instantiate(_soundPrefab, bgmPos);
            AudioSource audio = playSoundPos.GetComponent<AudioSource>();
            
            audio.loop = true;
            
            audio.clip = _bgmSounds[bgmName];
            audio.volume = volume;
            
            if(pitch == 0)
                audio.pitch = Random.Range(0.9f, 1.1f);
            else
                audio.pitch = pitch;
            
            audio.Play();
            _bgmTemp[bgmName] = audio;
        }
        
        public void StopBGM(string bgmName)
        {
            if (!_bgmTemp.ContainsKey(bgmName))
            {
                Debug.LogWarning($"Sound {bgmName} not found");
                return;
            }
            
            AudioSource audio = _bgmTemp[bgmName];
            audio.Stop();
        }

        public void PlaySFX(string soundName, Transform soundPos, bool isLoop = false, float volume = 1f, float pitch = 0f, float soundLength = 0.1f)
        {
            if (!_sfxSounds.ContainsKey(soundName))
            {
                Debug.LogWarning($"Sound {soundName} not found");
                return;
            }
            
            GameObject playSoundPos = Instantiate(_soundPrefab, soundPos);
            AudioSource audio = playSoundPos.GetComponent<AudioSource>();
            
            audio.loop = isLoop;
            audio.clip = _sfxSounds[soundName];
            audio.volume = volume;
            
            if(pitch == 0)
                audio.pitch = Random.Range(0.9f, 1.1f);
            else
                audio.pitch = pitch;
            
            audio.Play();
            
            if(!isLoop)
                Destroy(playSoundPos, audio.clip.length + soundLength);
        }
        
        public void PlaySFX(string soundName, Transform soundPos, float soundDistanceMax, float soundDistanceMin, bool isLoop = false)
        {
            if (!_sfxSounds.ContainsKey(soundName))
            {
                Debug.LogWarning($"Sound {soundName} not found");
                return;
            }
            
            GameObject playSoundPos = Instantiate(_soundPrefab, soundPos);
            AudioSource audio = playSoundPos.GetComponent<AudioSource>();
            
            audio.clip = _sfxSounds[soundName];
            
            audio.maxDistance = soundDistanceMax;
            audio.minDistance = soundDistanceMin;
            
            audio.loop = isLoop;
            
            audio.Play();
            
            if(!isLoop)
                Destroy(playSoundPos, audio.clip.length + 0.1f);
        }
        
        public int GetRandomSFX(int min, int max)
        {
            return Random.Range(min, max + 1);
        }
    }
}