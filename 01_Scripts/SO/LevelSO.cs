using UnityEngine;

namespace _01_Scripts.SO
{

    public enum LevelType
    {
        None,
        
        Factory,
        Tunnel,
        Mart,
        Shop,
        Mine,
        School,
        
        Max
    }
    
    [CreateAssetMenu(fileName = "LevelSO", menuName = "SO/Level/LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [Space]
        
        public LevelType LevelType;
        
        [Space]
        
        public PickObjectGroupGradeSO MapSo;
        public GameObject Wall;
        
        [Space]
        
        public PickObjectGroupGradeSO ItemSo;
        
        [Space]
        
        public PickObjectGroupGradeSO EntitySo;
        
    }
}