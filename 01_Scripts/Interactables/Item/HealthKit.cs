using UnityEngine;

namespace _01_Scripts.Interactables.Item
{
    public class HealthKit : PickUpable.PickUpable, IItem
    {
        [field:SerializeField] public bool IsDisposable { get; set; }
        public Player.Player Player { get; set; }
        public bool IsRightNow { get; set; }
        
        [SerializeField] private int healAmount;
        
        public void UseItem()
        {
            if (Player.Health.IsCurrentHealthMax == false)
            {
                Player.Health.HealHealth(healAmount);
                Destroy(gameObject);
            }
        }

        public void SecondaryUseItem()
        {
            
        }
    }
}