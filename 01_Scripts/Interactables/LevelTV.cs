using System.Collections.Generic;
using _01_Scripts.Level;
using _01_Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _01_Scripts.Interactables
{
    //레벨 선택 관련 담당
    public class LevelTV : MonoBehaviour
    {
        private bool _isPicked;
        
        [SerializeField] private Image _levelImage;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _levelTypeText;
        
        [SerializeField] private List<Sprite> _levelSprites = new List<Sprite>();
        
        [SerializeField] private LevelGroupSO _levelGroupSO;

        private LevelSO _levelSO;

        public void PickLevel()
        {
            if (!LevelManager.Instance.IsGameStart && !_isPicked)
            {
                _isPicked = true;
                LevelManager.Instance.LevelSo = _levelSO;
            }

            ResetLevel();
        }

        public void ResetLevel()
        {
            _isPicked = false;
            _levelSO = null;
            _levelImage.color = Color.black;
            _levelTypeText.text = "";
            _levelText.text = "";
        }
        
        public void SetLevel()
        {
            LevelSO levelSo = _levelGroupSO.Levels[Random.Range(0,_levelGroupSO.Levels.Length)];
            
            _levelSO = levelSo;
            _levelImage.color = Color.white;
            _levelImage.sprite = GetLevelType(levelSo.LevelType);
            _levelTypeText.text = GetLevelTypeString(levelSo.LevelType);

            if (levelSo.LevelType == LevelType.Shop)
                _levelText.text = "";
            else
                _levelText.text = "Level " + LevelManager.Instance.MapLevel;
            
        }
        

        private Sprite GetLevelType(LevelType levelType)
        {
            switch (levelType)
            {
                case LevelType.Factory:
                    return _levelSprites[0];
                case LevelType.Tunnel:
                    return _levelSprites[1]; 
                case LevelType.Mart:
                    return _levelSprites[2];
                case LevelType.Shop:
                    return _levelSprites[3];
                case LevelType.Mine:
                    return _levelSprites[4];
                case LevelType.School:
                    return _levelSprites[5];
                default:
                    return null;
            }
        }
        
        private string GetLevelTypeString(LevelType levelType)
        {
            switch (levelType)
            {
                case LevelType.Factory:
                    return "Factory";
                case LevelType.Tunnel:
                    return "Tunnel"; 
                case LevelType.Mart:
                    return "Mart";
                case LevelType.Shop:
                    return "Shop";
                case LevelType.Mine:
                    return "Mine";
                case LevelType.School:
                    return "School";
                default:
                    return null;
            }
        }
    }
}