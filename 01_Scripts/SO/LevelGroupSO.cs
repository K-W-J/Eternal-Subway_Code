using UnityEngine;

namespace _01_Scripts.SO
{
    [CreateAssetMenu(fileName = "LevelGroupSO", menuName = "SO/Level/LevelGroupSO", order = 0)]
    public class LevelGroupSO : ScriptableObject
    {
        public LevelSO[] Levels;
    }
}