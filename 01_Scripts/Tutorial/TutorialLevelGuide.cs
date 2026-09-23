using System.Collections.Generic;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Level;
using _01_Scripts.UI;
using TMPro;
using UnityEngine;

namespace _01_Scripts.Tutorial
{
    public class TutorialLevelGuide : MonoSingleton<TutorialLevelGuide>
    {
        [SerializeField] private GameObject worldCanvas;
        [SerializeField] private GameObject tutorialUIPrefab;
        
        [SerializeField] private GameObject reward;
        
        [SerializeField] private TextMeshProUGUI tutorialText;
        
        [SerializeField] private Transform[] tutorialPoint;
        
        private List<GameObject> tutorialUIList = new List<GameObject>();
        
        public int tutorialNumber;

        private void Start()
        {
            if(LevelManager.Instance.MapLevel == 0)
                TutorialNumberCount();
        }

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelCleared += LevelClear;
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelCleared -= LevelClear;
        }
        
        private void LevelClear()
        {
            foreach (Transform child in worldCanvas.transform)
            {
                if (child.TryGetComponent<LookAtPlayerUI>(out var lookAtPlayerUI))
                {
                    if(lookAtPlayerUI.IsTutorial)
                        Destroy(child.gameObject);
                }
            }

            if (LevelManager.Instance.MapLevel == 1)
            {
                tutorialNumber = 5;
                TutorialNumberCount();
                TutorialUI(reward.GetComponentInChildren<PickUpable>().transform, "Gift :)");
                reward.SetActive(true);
                
                LevelManager.Instance.OnLevelCleared -= LevelClear;
            }
        }
        
        public void TutorialUI(Transform target, string message)
        {
            GameObject tutorialUI = Instantiate(tutorialUIPrefab, worldCanvas.transform);
            LookAtPlayerUI lookAtPlayerUI = tutorialUI.GetComponent<LookAtPlayerUI>();
            
            lookAtPlayerUI.Target = target;
            lookAtPlayerUI.SetText(message);
            
            tutorialUIList.Add(tutorialUI);
        }

        public void TutorialNumberCount()
        {
            tutorialNumber++;

            foreach (GameObject tutorialUI in tutorialUIList)
                Destroy(tutorialUI);
            
            tutorialUIList.Clear();
            
            switch (tutorialNumber)
            {
                case 1:
                {
                    TutorialUI(tutorialPoint[0], "[E] Select Level");
                    TutorialUI(tutorialPoint[1], "[E] Select Level");
                    TutorialUI(tutorialPoint[2], "[E] Select Level");
                    tutorialText.text = "• Choose your destination.";
                    break;
                }
                case 2:
                {
                    tutorialText.text = "• Gather the trash.";
                    break;
                }
                case 3:
                {
                    TutorialUI(tutorialPoint[3], "[G] Drop Item");
                    tutorialText.text = "• Feed the compactor.\nIt hungers.";
                    break;
                }
                case 4:
                {
                    TutorialUI(tutorialPoint[4], "[E] Refuel");
                    tutorialText.text = "• Press the button.";
                    break;
                }
                case 5:
                {
                    TutorialUI(tutorialPoint[5],"[E] Depart Subway");
                    tutorialText.text = "• Start the subway...\ntutorial is over.";
                    break;
                }
                case 6:
                {
                    tutorialText.text = "";
                    break;
                }
            }
        }
    }
}