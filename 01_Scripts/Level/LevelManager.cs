using System;
using _01_Scripts.Interactables;
using _01_Scripts.SO;
using _01_Scripts.Tutorial;
using UnityEngine;

namespace _01_Scripts.Level
{
    //한 레벨의 전반적인 시스템 관리(레벨 시작, 레벨 종료, 다음 레벨 로드 등)
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public Action OnLevelCleared;
        public Action OnLevelStarted;
        public LevelSO LevelSo { get; set; }
        
        public System.Random LevelSeed { get; private set; }
        [field:SerializeField] public int Seed { get; private set; }

        [SerializeField] private LevelGroupSO _levelGroupSO;
        
        [SerializeField] private GameObject TimeOverEnemy;
        
        [SerializeField] private LevelTV[] _levelTV;
        
        [SerializeField] private Transform gameOverEnemyTransform;
        
        [SerializeField] private Transform[] soundPoint;
        
        [Space]
        
        public int MapLevel;
        public int MapCount;
        public int EntityCount;
        public int ItemCount;

        public int FuelNeeded;
        public int CurrentFuel;
        
        public bool IsGameStart;
        
        public float LevelMaxTime = 0;
        
        private float _levelCurrentTime;
        
        private float _ratio = 0.2f;

        private bool _isTimeOver;

        private void Awake()
        {
            if (Seed == 0)
                Seed = DateTime.Now.GetHashCode();

            LevelSeed = new System.Random(Seed);
        }

        private void Start()
        {
            ResetLevel();
                        
            foreach (var levelTV in _levelTV)
            {
                levelTV.SetLevel();
            }
        }

        private void Update()
        {
            LevelTimer();
        }

        private void LevelTimer()
        {
            if(MapLevel == 0 || !IsGameStart) return;
            
            GameManager.Instance.TimeText.text = $"CurrentTime : {(int)_levelCurrentTime} / LevelTime : {LevelMaxTime}";
            
            if (LevelMaxTime > _levelCurrentTime)
            {
                _levelCurrentTime += Time.deltaTime;
            }
            else
            {
                if (!_isTimeOver)
                {
                    _isTimeOver = true;
                    SoundManager.Instance.PlaySFX("Alarm", soundPoint[1], 10, 5, true);
                    _levelCurrentTime = LevelMaxTime;
                    Instantiate(TimeOverEnemy, gameOverEnemyTransform);
                }
            }
        }
        
        public void LevelClear()
        {
            if(!IsGameStart) return;
            
            if (MapLevel == 0 || MapLevel == 1)
            {
                TutorialLevelGuide.Instance.TutorialNumberCount();
            }

            GameManager.Instance.TimeText.text = "ERROR";
            
            PlayableDirectorManager.Instance.PlayTimeline("LevelClear");
            
            LevelUp();
            
            foreach (var levelTV in _levelTV)
            {
                levelTV.SetLevel();
            }
            
            SoundManager.Instance.PlaySFX("SubwaySound2", soundPoint[0]);
            
            GameManager.Instance.Wall.SetActive(true);
            IsGameStart = false;
            
            OnLevelCleared?.Invoke();
        }


        public void LevelStart()
        {
            if(IsGameStart) return;

            if (MapLevel == 0)
            {
                TutorialLevelGuide.Instance.TutorialNumberCount();
            }
            
            if (FuelNeeded > CurrentFuel)
            {
                MapLevel = 0;
                CurrentFuel = 10;
                FuelNeeded = 10;
                GameManager.Instance.GameOver();
                return;
            }
            
            PlayableDirectorManager.Instance.PlayTimeline("LevelStart");

            foreach (var levelTV in _levelTV)
            {
                levelTV.ResetLevel();
            }
            
            SoundManager.Instance.PlaySFX("SubwaySound3", soundPoint[0]);
            
            CurrentFuel -= FuelNeeded;
            
            GameManager.Instance.Wall.SetActive(false);
            
            GameManager.Instance.FuelText.text = $"CurrentFuel : {CurrentFuel} / FuelNeeded : {FuelNeeded}";
            
            IsGameStart = true;
            
            OnLevelStarted?.Invoke();
        }


        private void ResetLevel()
        {
            GameManager.Instance.FuelText.text = $"CurrentFuel : {CurrentFuel} / FuelNeeded : {FuelNeeded}";
            
            _levelCurrentTime = 0;
        }
        
        private void LevelUp()
        {
            EntityCount += Mathf.CeilToInt(EntityCount * _ratio);
            
            if(MapLevel < 5)
                MapCount += Mathf.CeilToInt(MapCount * _ratio);
            
            ItemCount += Mathf.CeilToInt(ItemCount * _ratio);
            LevelMaxTime += Mathf.CeilToInt(LevelMaxTime * _ratio);
            FuelNeeded += Mathf.CeilToInt(FuelNeeded * _ratio);
            
            if (MapLevel == 0)
            {
                EntityCount = 1;
                LevelMaxTime = 180;
            }
            
            MapLevel += 1;

            ResetLevel();
        }
    }
}