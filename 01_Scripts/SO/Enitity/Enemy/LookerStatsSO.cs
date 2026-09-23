using UnityEngine;

namespace _01_Scripts.SO.Enitity.Enemy
{
    
    [CreateAssetMenu(fileName = "LookerStatsSO", menuName = "SO/Entity/Enemy/LookerStatsSO", order = 0)]
    public class LookerStatsSO : LongRangeEnemyStatsSO
    {
        [Space]
        
        public float LookTime;
        
        [Space]
        
        public float RandomChasePos;
        public float minRemainingChaseTime;
        public float maxRemainingChaseTime;
        public float RandomAttackPos;
        public float minRemainingAttackTime;
        public float maxRemainingAttackTime;

        private void OnValidate()
        {
            
        }
    }
}