using System;
using UnityEngine;

namespace _01_Scripts.Enemy.Looker
{
    public class LookCounter : MonoBehaviour
    {
        public Action OnLookCount;
        public int LookCount { get; set; }

        public void SetLookCount()
        {
            if (LookCount > 4) return;

            //LookCount += 1;
            OnLookCount?.Invoke();
            
            return;

            switch (LookCount)
            {
                case 1:
                {
                    SoundManager.Instance.PlaySFX("Three", transform);
                    break;
                }
                case 2:
                {
                    SoundManager.Instance.PlaySFX("Two", transform);
                    break;
                }
                case 3:
                {
                    SoundManager.Instance.PlaySFX("One", transform);
                    break;
                }
                case 4:
                {
                    SoundManager.Instance.PlaySFX("Zero", transform);
                    LookCount += 1;
                    break;
                }
            }
        }
    }
}