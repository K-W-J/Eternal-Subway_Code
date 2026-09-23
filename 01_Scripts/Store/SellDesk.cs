using System.Collections.Generic;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Level;
using TMPro;
using UnityEngine;

namespace _01_Scripts.Store
{
    public class SellDesk : MonoBehaviour
    {
        [SerializeField] private Store _store;
        
        [SerializeField] private Transform _sellZone;
        [SerializeField] private Vector3 _sellZoneSize;
        
        [SerializeField] private TextMeshProUGUI totalPriceText;
        
        private List<PickUpable> _sellItems = new List<PickUpable>();

        private void Update()
        {
            CheckSellItem();
        }

        private void SaveSellItemData(PickUpable sellItem)
        {
            if(_sellItems.Contains(sellItem)) return;
                
            _sellItems.Add(sellItem);
            _store.PriceUISetting(sellItem, false);
        }

        public void SellItem()
        {
            SoundManager.Instance.PlaySFX("BuyAndSell", transform);
            
            int sellItemCount = _sellItems.Count;

            for (int i = 0; i < sellItemCount; i++)
            {
                if(_sellItems[i].PickUpableSo.SellPrice == 0) continue;
                
                GameManager.Instance.Player.PlayerMoney.
                    AddMoney(_sellItems[i].PickUpableSo.SellPrice);
                
                Destroy(_sellItems[i].gameObject);
            }
            
            _sellItems.Clear();
        }

        private void CheckSellItem()
        {
            var colliders = Physics.OverlapBox(_sellZone.position, _sellZoneSize * 0.5f);

            int sum = 0;
            
            foreach (var overlap in colliders)
            {
                if (overlap.TryGetComponent<PickUpable>(out var pickupable))
                {
                    sum += pickupable.PickUpableSo.SellPrice;
                    SaveSellItemData(pickupable);
                }
            }

            CheckSellZone(colliders);
            
            totalPriceText.text = sum + "$";
        }

        private void CheckSellZone(Collider[] colliders)
        {
            for (int i = _sellItems.Count - 1; i >= 0; i--)
            {
                bool isOverlap = false;
                
                foreach (var overlap in colliders)
                {
                    if (overlap.gameObject == _sellItems[i].gameObject && !_sellItems[i].IsPickUp)
                        isOverlap = true;
                }

                if (!isOverlap)
                {
                    _sellItems[i].DestroyMarkUI();
                    _sellItems.Remove(_sellItems[i]);
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_sellZone.position, _sellZoneSize);
        }
    }
}