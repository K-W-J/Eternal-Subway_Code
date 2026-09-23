using _01_Scripts.Level;
using UnityEngine;

namespace _01_Scripts.Tutorial
{
    public class TutorialPress : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(LevelManager.Instance.MapLevel == 0 && TutorialLevelGuide.Instance.tutorialNumber == 3)
                TutorialLevelGuide.Instance.TutorialNumberCount();
        }
    }
}