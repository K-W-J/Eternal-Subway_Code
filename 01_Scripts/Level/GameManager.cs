using System.Collections.Generic;
using _01_Scripts.Interactables;
using _01_Scripts.Interactables.Subway;
using TMPro;
using UnityEngine;


namespace _01_Scripts.Level
{
    //게임의 전반적인 시스템을 담당
    public class GameManager : MonoSingleton<GameManager> 
    {
        [field:SerializeField] public List<Transform> LookerSpawnPoints { get; set; } = new List<Transform>();
        [field:SerializeField] public Player.Player Player { get; set; }
        [field:SerializeField] public SubwayChecker SubwayChecker { get; private set; }
        
        [field:SerializeField] public TextMeshProUGUI FuelText { get; set; }
        [field:SerializeField] public TextMeshProUGUI TimeText { get; set; }
        [field:SerializeField] public GameObject Wall { get; set; }
        
        [field:SerializeField] public GameObject Over { get; set; }
        
        public bool IsGameOver { get; private set; }

        private void Awake()
        {
            int width = 1280;
            int height = 720;

            Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
            
            Time.timeScale = 1;
        }
        
        private void OnEnable()
        {
            Player.OnGameOver += GameOver;
            LevelManager.Instance.OnLevelCleared += GameClear;
            LevelManager.Instance.OnLevelStarted += GameClear;
        }

        private void OnDisable()
        {
            Player.OnGameOver -= GameOver;
            LevelManager.Instance.OnLevelCleared -= GameClear;
            LevelManager.Instance.OnLevelStarted -= GameClear;
        }
        
        public void GameOver()
        {            
            Over.SetActive(true);
            Cursor.visible = true;
            LevelManager.Instance.IsGameStart = false;
            Time.timeScale = 0;
            
            if(Wall != null)
                Wall.SetActive(true);
        }

        private void GameClear()
        {
            /*if(!VolumeManager.Instance.IsFadeRuning)
                StartCoroutine(VolumeManager.Instance.FadeOut());*/
        }
    }
}
