using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts.Level
{
    public class EntityManager : MonoBehaviour
    {
        private List<GameObject> _entities;
        private PickRandomObjectGrade _probability;
        
        private void Start()
        {
            if (null == _probability)
            {
                _probability = new PickRandomObjectGrade(LevelManager.Instance.LevelSeed);
            }
            
            _entities = new List<GameObject>();
            
        }
        private void OnEnable()
        {
            MapManager.Instance.OnMapCreateComplete += SpawnEntities;
            LevelManager.Instance.OnLevelCleared += ResetEntities;
        }

        private void OnDisable()
        {
            MapManager.Instance.OnMapCreateComplete -= SpawnEntities;
            LevelManager.Instance.OnLevelCleared -= ResetEntities;
        }
        
        private void ResetEntities()
        {
            StartCoroutine(CreatorDaley());
        }
        
        private IEnumerator CreatorDaley()
        {
            yield return new WaitForSeconds(0.01f);
            
            foreach (var entity in _entities)
            {
                Destroy(entity);
            }
        
            _entities.Clear();
        }

        private void SpawnEntities()
        {
            if(MapManager.Instance.EntityPoint.Count <= 0) return;
            
            _probability.GroupGradeSetting(LevelManager.Instance.LevelSo.EntitySo);
            
            for (int i = 0; i < LevelManager.Instance.EntityCount; i++)
            {
                int random = Random.Range(0, MapManager.Instance.EntityPoint.Count);
                
                GameObject entity = Instantiate(_probability.PickRandomObject(LevelManager.Instance.LevelSo.EntitySo), MapManager.Instance.EntityPoint[random]);
                entity.transform.SetParent(transform);
                _entities.Add(entity);
            }
        }
    }
}