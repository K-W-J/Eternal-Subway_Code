using System;
using UnityEngine;

namespace _01_Scripts.Define
{
    [Serializable]
    public class PatrolAndChaseSo
    {
        [Space]
        
        public float PatrolSpeed;
        public float ChaseSpeed;
        public float ChaseTime;
        public int DetectRange;
    }
    
    [Serializable]
    public class AttackSo
    {
        [Space]
        
        public float AttackRange;
        public float AttackDelay;
    }
    
    [Serializable]
    public class LongRangeAttackSo
    {
        [Space]
        
        public float LongRangeAttackRange;
        public float LongRangeAttackTime;
    }

    [Serializable]
    public class TrapSo
    {
        [Space]
        
        public float TarpDelayMin;
        public float TarpDelayMax;
    }
}