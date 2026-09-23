using _01_Scripts.Interactables.PickUpable;
using UnityEngine;

namespace _01_Scripts.Store
{
    public class ItemStand : MonoBehaviour
    {
        [SerializeField] private Transform saleItemPoint;
        
        private PickUpable _item;

        public void BuyItemSale()
        {
            _item.BuyItem();
            _item = null;
        }
        
        public bool ItemStandCheck()
        {
            if (_item == null)
                return true;
            
            return false;
        }
        
        public void SettingItemStand(PickUpable item)
        {
            item.transform.position = saleItemPoint.position;
            _item = item;
        }


        public void DestroyItem()
        {
            if(_item == null || !_item.IsSaleItem) return;
            
            Destroy(_item.gameObject);
            _item = null;
        }
    }
}