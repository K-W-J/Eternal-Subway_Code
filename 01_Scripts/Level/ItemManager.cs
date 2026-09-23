using System.Collections;
using System.Collections.Generic;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Interactables.Subway;
using _01_Scripts.Tutorial;
using UnityEngine;

namespace _01_Scripts.Level
{
    public class ItemManager : MonoBehaviour
    {
        private List<GameObject> _items;
    
        private PickRandomObjectGrade _probability;

        private void Start()
        {
            if (null == _probability)
            {
                _probability = new PickRandomObjectGrade(LevelManager.Instance.LevelSeed);
            }
            
            _items = new List<GameObject>();
        }

        private void OnEnable()
        {
            MapManager.Instance.OnMapCreateComplete += SpawnItems;
            LevelManager.Instance.OnLevelCleared += ResetItems;
        }

        private void OnDisable()
        {
            MapManager.Instance.OnMapCreateComplete -= SpawnItems;
            LevelManager.Instance.OnLevelCleared -= ResetItems;
        }
    
        private void ResetItems()
        {
            StartCoroutine(CreatorDaley());
        }
        
        private IEnumerator CreatorDaley()
        {
            yield return new WaitForSeconds(0.01f);
            
            foreach (var item in _items)
            {
                if (item == null || item.TryGetComponent<PickUpable>(out var pickupable) == false) continue;
                    
                if(pickupable.IsPickUp == false)
                    Destroy(item);
            }
        
            _items.Clear();
        }

        private void SpawnItems()
        {
            if(MapManager.Instance.ItemPoint.Count <= 0) return;
            
            _probability.GroupGradeSetting(LevelManager.Instance.LevelSo.ItemSo);
        
            for (int i = 0; i < LevelManager.Instance.ItemCount; i++)
            {
                int random = UnityEngine.Random.Range(0, MapManager.Instance.ItemPoint.Count);
                
                Vector3 spawnPos = Vector3.zero;

                if (MapManager.Instance.ItemPoint[random] != null)
                {
                     spawnPos = MapManager.Instance.ItemPoint[random].position;
                }
                else
                {
                    foreach (var itemPoint in MapManager.Instance.ItemPoint)
                    {
                        if (itemPoint != null)
                        {
                            spawnPos = itemPoint.position;
                            break;
                        }
                    }
                }
                
                GameObject item = Instantiate(_probability.PickRandomObject(LevelManager.Instance.LevelSo.ItemSo), spawnPos, Quaternion.identity);
                
                if (LevelManager.Instance.MapLevel == 0)
                {
                    TutorialLevelGuide.Instance.TutorialUI(item.GetComponentInChildren<PickUpable>().transform, "[E] Take Item");
                }
                
                item.transform.SetParent(transform);
                _items.Add(item);
            }
        }
    }
}
