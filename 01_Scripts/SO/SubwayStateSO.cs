using UnityEngine;

namespace _01_Scripts.SO
{
    [CreateAssetMenu(fileName = "SubwayStateSO", menuName = "SO/Subway/SubwayStateSO", order = 0)]
    public class SubwayStateSO : ScriptableObject
    {
        public int MaxHealth;
        public int MaxFuel;
    }
}