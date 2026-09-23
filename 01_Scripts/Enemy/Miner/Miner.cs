
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;

namespace _01_Scripts.Enemy.Miner
{
    public class Miner : Enemy
    {
        [field:SerializeField] public BulletSponsor BulletSponsor { get; private set; }
        [field:SerializeField] public Transform FirePos;
        public MinerStatsSO MinerStatsSO => BaseEnemyStatsSo as MinerStatsSO;
        
    }
}