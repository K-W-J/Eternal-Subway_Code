using UnityEngine;

namespace _01_Scripts.SO.Enitity.Enemy
{
    public enum EnemeyType
    {
        None = -1,
    
        Miner,
        Clown,
        Snailman,
        Looker,
        Minder,
        MaskMan,
    
        Max
    }
    
    [CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "SO/Entity/Enemy/EnemyStatsSO", order = 0)]
    public class BaseEnemyStatsSO : EntityStatsSO
    {
        [Space]
        
        public EnemeyType EnemeyType;
        public int AttackPower;
        
        [Space]
        
        public float PatrolSpeed;
        public float ChaseSpeed;
        public float ChaseTime;
        public int DetectRange;
        
        [Space]
        
        public float AttackRange;
        public float AttackDelay;
    }
}