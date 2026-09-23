using _01_Scripts.Define;
using UnityEngine;

namespace _01_Scripts.SO.Enitity.Enemy
{
    
    [CreateAssetMenu(fileName = "ClownStatsSO", menuName = "SO/Entity/Enemy/ClownStatsSO", order = 0)]
    public class ClownStatsSO : BaseEnemyStatsSO
    {
        [Space]
        
        public TrapSo TrapSo; 
    }
}