using _01_Scripts.Level;
using _01_Scripts.Tutorial;
using UnityEngine;

namespace _01_Scripts.Interactables.Subway
{
    public class HydraulicPlate : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(LevelManager.Instance.MapLevel == 0 && TutorialLevelGuide.Instance.tutorialNumber == 4)
                TutorialLevelGuide.Instance.TutorialNumberCount();
            
            if (other.TryGetComponent<PickUpable.PickUpable>(out var pickUpable))
            {
                LevelManager.Instance.CurrentFuel += pickUpable.PickUpableSo.Fuel;
                GameManager.Instance.FuelText.text = $"CurrentFuel : {LevelManager.Instance.CurrentFuel} / FuelNeeded : {LevelManager.Instance.FuelNeeded}";
            }

            if (other.gameObject.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}