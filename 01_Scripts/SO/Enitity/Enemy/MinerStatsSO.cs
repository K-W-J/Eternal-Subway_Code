using UnityEngine;

namespace _01_Scripts.SO.Enitity.Enemy
{
    
    [CreateAssetMenu(fileName = "MinerStatSO", menuName = "SO/Entity/Enemy/MinerStatSO", order = 0)]
    public class MinerStatsSO : LongRangeEnemyStatsSO
    {
        public GameObject Bullet;
    }
}