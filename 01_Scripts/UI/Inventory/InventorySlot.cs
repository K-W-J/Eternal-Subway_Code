using System;
using _01_Scripts.Interactables.PickUpable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _01_Scripts.UI.Inventory
{
    [Serializable]
    public class InventorySlot : MonoBehaviour
    {
        [field: SerializeField] public Image SeledImage { get; private set; }
        
        [SerializeField] private Image image;

        public PickUpable Slot { get; private set; }
        public bool IsSelect { get; set; }


        public bool InventorySlotCheck()
        {
            if (Slot == null)
                return true;
            
            return false;
        }

        public void SettingInventorySlot(PickUpable slot)
        {
            Slot = slot;
            image.sprite = Slot.PickUpableSo.ItemIcon;
        }

        public void SetSeled(bool isSelect)
        {
            IsSelect = isSelect;
            SeledImage.gameObject.SetActive(isSelect);

            if (isSelect)
            {
                transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.2f);
            }
            else
            {
                transform.DOScale(new Vector3(1f, 1f, 0f), 0.2f);
            }
        }
        
        public void SetActive(bool isActive) => Slot.gameObject.SetActive(isActive);

        public void ResetInventorySlot(Vector3 dropPos = default)
        {
            if (Slot != null)
                Slot.DropObject(dropPos);
            
            Slot = null;
            image.sprite = null;
        }
    }
}