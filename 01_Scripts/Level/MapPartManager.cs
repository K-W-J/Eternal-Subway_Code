using System;
using System.Collections.Generic;
using _01_Scripts.SO;
using UnityEngine;

namespace _01_Scripts.Level
{
        public class MapPartManager : MonoBehaviour
    {
        
        [field:Header("MapParts")]
        [field:SerializeField] public Transform MapPartSpawnPointGroup { get; private set; }
        [SerializeField] private List<Transform> _mapPartSpawnPoints;
        public List<Transform> MapPartSpawnPoints => _mapPartSpawnPoints;

        [field:SerializeField] public Transform MapPartStartPoint { get; private set; }
        
        [Header("ObjectSpawnPoints")]
        [SerializeField] private PickMapObjectGroupSO mapObjectGroupSo;
        
        [field:Space]
        [field:SerializeField] public Transform ObjectSpawnPointGroup { get; private set; }
        [SerializeField] private List<Transform> _objectSpawnPoints;
        
        [Header("EntitySpawnPoints")]
        [field:SerializeField] public Transform EntitySpawnPointGroup { get; private set; }
        [SerializeField] private List<Transform> _entitySpawnPoints;
        public List<Transform> EntitySpawnPoints => _entitySpawnPoints;
        
        [Header("ItemSpawnPoints")]
        [field:SerializeField] public Transform ItemSpawnPointGroup { get; private set; }
        [SerializeField] private List<Transform> _itemSpawnPoints;
        public List<Transform> ItemSpawnPoints => _itemSpawnPoints;
        
        [field:Header("GetComponent")]
        [field: SerializeField] public MapOverlapChecker OverlapChecker { get; private set; }
        
        private PickRandomObjectGrade _probability;

        public void Initialize()
        {
            Debug.Assert(LevelManager.Instance != null, "LevelManager is null");
            
            _probability = new PickRandomObjectGrade(LevelManager.Instance.LevelSeed);

            ObjectCreator();
        }

        private void ObjectCreator()
        {
            if (ObjectSpawnPointGroup != null)
            {
                for (int i = 0; i < _objectSpawnPoints.Count; i++)
                {
                    GameObject currentObject = Instantiate(SurfaceTypeCheck(_objectSpawnPoints[i]), 
                        _objectSpawnPoints[i].position, Quaternion.identity);
                    
                    currentObject.transform.SetParent(OverlapChecker.mapRander.transform);
                
                    currentObject.transform.rotation = _objectSpawnPoints[i].rotation * currentObject.transform.rotation;
                    currentObject.transform.position = _objectSpawnPoints[i].position;
                    
                }
            }
        }

        private GameObject SurfaceTypeCheck(Transform objectSpawnPoint)
        {
            if (objectSpawnPoint.gameObject.layer == LayerMask.NameToLayer("SpawnWall"))
            {
                _probability.GroupGradeSetting(mapObjectGroupSo.WallObjectGroup);
                return _probability.PickRandomObject(mapObjectGroupSo.WallObjectGroup);
            }
            else if (objectSpawnPoint.gameObject.layer == LayerMask.NameToLayer("SpawnFloor"))
            {
                _probability.GroupGradeSetting(mapObjectGroupSo.FloorObjectGroup);
                return _probability.PickRandomObject(mapObjectGroupSo.FloorObjectGroup);
            }
            else if (objectSpawnPoint.gameObject.layer == LayerMask.NameToLayer("SpawnCeiling"))
            {
                _probability.GroupGradeSetting(mapObjectGroupSo.CeilingObjectGroup);
                return _probability.PickRandomObject(mapObjectGroupSo.CeilingObjectGroup);
            }
            else
                return null;

        }
    }
}