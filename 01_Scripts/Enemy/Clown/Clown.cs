using System.Collections;
using _01_Scripts.SO.Enitity.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Enemy.Clown
{
    public class Clown : Enemy
    {
        [field:SerializeField] public TrapSponsor TrapSponsor { get; private set; }
        public ClownStatsSO ClownStatsSO => BaseEnemyStatsSo as ClownStatsSO;

        private bool isWaitSound;
        
        private void Update()
        {
            if(!isWaitSound)
                StartCoroutine(WaitSound());
        }

        private IEnumerator WaitSound()
        {
            isWaitSound = true;
            
            
            yield return new WaitForSeconds(Random.Range(3f, 10f));
            
            int rand =  SoundManager.Instance.GetRandomSFX(1, 3);
            SoundManager.Instance.PlaySFX("ManLaugh" + rand, transform);
            
            isWaitSound = false;
        }
    }
}