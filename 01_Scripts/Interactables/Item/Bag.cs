using UnityEngine;

namespace _01_Scripts.Interactables.Item
{
    public class Bag : PickUpable.PickUpable, IItem
    {
        public bool IsDisposable { get; set; }
        public bool IsRightNow { get; set; }
        public Player.Player Player { get; set; }
        public void UseItem()
        {
            
        }

        public void SecondaryUseItem()
        {
            
        }
    }
}