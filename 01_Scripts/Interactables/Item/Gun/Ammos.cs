using UnityEngine;

namespace _01_Scripts.Interactables.Item.Gun
{
    public class Ammos : PickUpable.PickUpable, IItem
    {
        [field:SerializeField] public bool IsRightNow { get; set; }
        public bool IsDisposable { get; set; }
        public Player.Player Player { get; set; }
        
        [SerializeField] private int ammo;

        public void UseItem()
        {
            Player.PlayerAmmo.AddAmmo(ammo);
            Destroy(gameObject);
        }

        public void SecondaryUseItem()
        {
            
        }
    }
}