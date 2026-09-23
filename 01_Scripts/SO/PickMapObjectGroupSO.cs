using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts.SO
{
    [CreateAssetMenu(fileName = "MapObjectSO", menuName = "SO/MapObjectSO", order = 0)]
    public class PickMapObjectGroupSO : ScriptableObject
    {
        [Header("Wall")] 
        public PickObjectGroupGradeSO WallObjectGroup;
        
        [Header("Floor")] 
        public PickObjectGroupGradeSO FloorObjectGroup;
            
        [Header("Ceiling")]
        public PickObjectGroupGradeSO CeilingObjectGroup;
    }
}