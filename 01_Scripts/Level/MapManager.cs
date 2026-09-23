using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace _01_Scripts.Level
{
    public class MapManager : MonoSingleton<MapManager>
    {
        public Action OnMapCreateComplete;
        
        public List<Transform> ItemPoint { get; private set; }
        public List<Transform> EntityPoint { get; private set; }
        
        [SerializeField] private List<GameObject> currentMapParts;
        [SerializeField] private Transform mapPartPrefab;
        [SerializeField] private NavMeshSurface navSurface;
        
        private List<Transform> _currentSpawnPoints;
        private List<Transform> _tempMapSpawnPoints;
        private List<GameObject> _walls;

        private PickRandomObjectGrade _probability;

        private void Awake()
        {
            _walls = new List<GameObject>();
            _tempMapSpawnPoints = new List<Transform>();
            _currentSpawnPoints = new List<Transform>();
            ItemPoint = new List<Transform>();
            EntityPoint = new List<Transform>();
            
            LevelManager.Instance.OnLevelCleared += ResetMapParts;
            LevelManager.Instance.OnLevelStarted += CreateMap;
        }
        
        private void OnDestroy()
        {
            LevelManager.Instance.OnLevelCleared -= ResetMapParts;
            LevelManager.Instance.OnLevelStarted -= CreateMap;
        }

        private void Start()
        {
            _probability ??= new PickRandomObjectGrade(LevelManager.Instance.LevelSeed);
        }
        
        private void ResetMapParts()
        {
            foreach (var currentMapPart in currentMapParts)
            {
                Destroy(currentMapPart);
            }
            
            foreach (var wall in _walls)
            {
                Destroy(wall);
            }
            
            _walls.Clear();
            currentMapParts.Clear();
            ItemPoint.Clear();
            EntityPoint.Clear();
        }

        private void CreateMap()
        {
            _probability.GroupGradeSetting(LevelManager.Instance.LevelSo.MapSo);
            
            _tempMapSpawnPoints.Add(mapPartPrefab);
            StartCoroutine(MapCreator(LevelManager.Instance.MapCount));
        }

        private IEnumerator MapCreator(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _currentSpawnPoints.AddRange(_tempMapSpawnPoints);
                _tempMapSpawnPoints.Clear();
            
                foreach (var endPoint in _currentSpawnPoints)
                {
                    GameObject mapObject = Instantiate(_probability.PickRandomObject(LevelManager.Instance.LevelSo.MapSo), Vector3.zero, Quaternion.identity);
                
                    MapPartManager mapPartManager = mapObject.GetComponent<MapPartManager>();
                
                    //이전 맵 파츠 EndPoint와 현재 맵 파츠 StartPoint의 위치와 회전을 계산하여 두점을 겹치게 만든다.
                    mapObject.transform.rotation = endPoint.rotation * mapPartManager.MapPartStartPoint.rotation;
                    mapObject.transform.position = endPoint.position - mapPartManager.MapPartStartPoint.position; 

                    if (!mapPartManager.OverlapChecker.OverlapCheck())
                    {
                        mapPartManager.Initialize();
                        currentMapParts.Add(mapObject);
                        mapObject.transform.SetParent(transform);
                    
                        //맵이 안겹쳤다면 자신의 모든 SpawnPoint를 맵매니저 리스트에 넣어준다.
                        //맵이 안겹쳤다면 자신의 모든 EntitySpawnPoint를 맵매니저 리스트에 넣어준다.
                        //맵이 안겹쳤다면 자신의 모든 ItemPoint를 맵매니저 리스트에 넣어준다.
                        GetPointPos(mapPartManager.MapPartSpawnPoints, _tempMapSpawnPoints);
                        GetPointPos(mapPartManager.EntitySpawnPoints, EntityPoint);
                        GetPointPos(mapPartManager.ItemSpawnPoints, ItemPoint);
                    }
                    else
                    {
                        Destroy(mapObject);
                        CloseMapBoundaries(endPoint);
                    }
                    
                    yield return null;
                }
            }

            foreach (var tempMapSpawnPoint in _tempMapSpawnPoints)
            {
                CloseMapBoundaries(tempMapSpawnPoint);
            }
        
            _tempMapSpawnPoints.Clear();    

            //LevelManager의 맵 파츠 갯수에 따라 
            if (currentMapParts.Count < LevelManager.Instance.MapCount)
            {
                ResetMapParts();
                CreateMap();
            }
            else
            {
                //맵 생성이 완료
                navSurface.BuildNavMesh();
                OnMapCreateComplete?.Invoke();
                Debug.Log("Map Generation Completed Successfully.");
            }
        }

        private void GetPointPos(List<Transform> pointsPos, List<Transform> savePointPos)
        {
            foreach (var pointPos in pointsPos)
            {
                if(pointPos == null)
                    break;
                            
                savePointPos.Add(pointPos);
            }
        }

        private void CloseMapBoundaries(Transform mapEndPoint)
        {
            GameObject wall = Instantiate(LevelManager.Instance.LevelSo.Wall, Vector3.zero, Quaternion.identity);
            wall.transform.rotation = mapEndPoint.rotation * Quaternion.Euler(0, 90, 0);
            wall.transform.position = mapEndPoint.position; 
            wall.transform.SetParent(transform);
            _walls.Add(wall);
        }
    }
}
