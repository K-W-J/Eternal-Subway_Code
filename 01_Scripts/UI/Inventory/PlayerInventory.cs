using System.Collections.Generic;
using _01_Scripts;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Player;
using _01_Scripts.UI.Inventory;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCount;
    
    [SerializeField] private Transform inventoryPanel;

    [SerializeField] private GameObject _inventorySolt;
    
    [SerializeField] private Player _agent;

    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();
    
    private int _currentSelectSlot;
    
    private void Start()
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            GameObject slot = Instantiate(_inventorySolt, inventoryPanel);
            _inventorySlots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public PickUpable GetSoltItem()
    {
        if(!IsSlotSelected()) return null;

        return _inventorySlots[_currentSelectSlot].Slot;
    }

    public void SelectSolt(int selectSlot)
    {
        if (_currentSelectSlot == selectSlot)
        {
            if (_inventorySlots[_currentSelectSlot].IsSelect)
            {
                SetSeledSlot(false);
            }
            else
            {
                SetSeledSlot(true);
            }
        }
        else
        {
            SetSeledSlot(false);
            
            _currentSelectSlot = selectSlot;
            
            SetSeledSlot(true);
        }
    }


    public void PutInInventory(PickUpable item)
    {
        if (!InventoryCheck()) return;
        
        SoundManager.Instance.PlaySFX("ItemPickup",transform);

        if (_inventorySlots[_currentSelectSlot].InventorySlotCheck() && _inventorySlots[_currentSelectSlot].IsSelect)
        {
            _inventorySlots[_currentSelectSlot].SettingInventorySlot(item);
            
            _agent.PlayerUse.PickUpItem(_inventorySlots[_currentSelectSlot].Slot);
            
            return;
        }
            
        for (int i = 0; i < inventoryCount; i++)
        {
            if (_inventorySlots[i].InventorySlotCheck())
            {
                _inventorySlots[i].SettingInventorySlot(item);
                _inventorySlots[i].SetActive(false);
                
                break;
            }
        }
    }
    
    public void TakeOutInventory(Vector3 dropPos)
    {
        if(_inventorySlots[_currentSelectSlot].InventorySlotCheck() || !_inventorySlots[_currentSelectSlot].IsSelect) return;
        
        SoundManager.Instance.PlaySFX("ItemPickup",transform);

        _inventorySlots[_currentSelectSlot].SetActive(true);

        ResetSlotItem(dropPos);
    }
    
    public bool InventoryCheck()
    {
        foreach (var inventorySlot in _inventorySlots)
        {
            if(inventorySlot.InventorySlotCheck())
                return true;
        }
        
        return false;
    }

    public void ResetSlotItem(Vector3 dropPos = default)
    {
        _inventorySlots[_currentSelectSlot].ResetInventorySlot(dropPos);
    }

    public bool IsSlotSelected()
    {
        return _inventorySlots[_currentSelectSlot].IsSelect;
    }

    public bool IsSelectedSlotEmpty()
    {
        return _inventorySlots[_currentSelectSlot].InventorySlotCheck();
    }
    
    public PickUpable GetInventorySlotObject()
    {
        return _inventorySlots[_currentSelectSlot].Slot;
    }
    private void SetSeledSlot(bool isSeled)
    {
        _inventorySlots[_currentSelectSlot].SetSeled(isSeled);

        if (!_inventorySlots[_currentSelectSlot].InventorySlotCheck())
        {
            _inventorySlots[_currentSelectSlot].SetActive(isSeled);
            _agent.PlayerUse.PickUpItem(_inventorySlots[_currentSelectSlot].Slot);
        }
    }
}
