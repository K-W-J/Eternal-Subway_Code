using System;
using UnityEngine;

namespace _01_Scripts.Entity.EntityState
{
    [CreateAssetMenu(fileName = "StateSo", menuName = "SO/StateSo", order = 0)]
    public class StateSo : ScriptableObject
    {
        public EntityStateType stateType;
        public string className;
        public string animationName;

        public int animationHash;
        
        //인스펙터를 볼 때마다 드롭다운이 0번(첫 번째 항목)으로 초기화하므로 enum의 인덱스 저장
        [SerializeField] private int _stateIndex;

        private void OnValidate()
        {
            if(animationName != null)
                animationHash = Animator.StringToHash(animationName);
        }
    }
}