using UnityEngine;

namespace _01_Scripts.SO.Enitity
{
    [CreateAssetMenu(fileName = "EntityStatsSO", menuName = "SO/Entity/EntityStatSO", order = 0)]
    public class EntityStatsSO : ScriptableObject
    {
        public int MaxHealth;

        private void OnValidate()
        {
            if(MaxHealth <= 0)
                MaxHealth = 1;
        }
    }
}