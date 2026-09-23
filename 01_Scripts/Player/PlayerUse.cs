using _01_Scripts.Interactables.Item;
using _01_Scripts.Interactables.Item.Gun;
using _01_Scripts.Interactables.PickUpable;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerUse : MonoBehaviour
    {
        public bool IsUsing { get; private set; }
        
        [SerializeField] private Player _agent;
        
        [SerializeField] private Transform _handPoint;
        
        [SerializeField] private Transform _dropPos;

        private int _tempAmmo;
        
        private Transform _item;
        private bool _isFirstClicking;

        private void OnEnable()
        {
            _agent.PlayerInputSo.OnDropItemAction += OnDropItem;
            _agent.PlayerInputSo.OnUseAction += OnUse;
            _agent.PlayerInputSo.OnSecondaryUseAction += OnSecondaryUse;
            _agent.PlayerInputSo.OnGunLoadAction += OnGunLoad;
        }

        private void OnDisable()
        {
            _agent.PlayerInputSo.OnDropItemAction -= OnDropItem;
            _agent.PlayerInputSo.OnUseAction -= OnUse;
            _agent.PlayerInputSo.OnSecondaryUseAction -= OnSecondaryUse;
            _agent.PlayerInputSo.OnGunLoadAction -= OnGunLoad;
        }
        
        
        public void PickUpItem(PickUpable pickRandom)
        {
            pickRandom.transform.SetParent(_handPoint);
            pickRandom.transform.position = _handPoint.position;
            pickRandom.transform.rotation = _handPoint.rotation;
            _item = pickRandom.transform;
        }

        public void OnDropItem()
        {
            if(_item == null) return;
            
            if (_item.transform.parent != _handPoint && _item.transform.parent != _agent.AimingPoint)
            {
                _item.transform.parent.SetParent(null);
            }
            else
            {
                _item.transform.SetParent(null);
            }
            
            _item = null;
            _agent.PlayerInventory.TakeOutInventory(_dropPos.position);
        }
        private void OnSecondaryUse(bool obj)
        {
            if(_agent.PlayerInventory.GetInventorySlotObject() is not Pistol) return;
            
            IsUsing = obj;
            
            if (obj && _agent.PlayerInventory.IsSlotSelected() && !_agent.PlayerInventory.IsSelectedSlotEmpty())
            {
                if (_agent.PlayerInventory.GetInventorySlotObject().gameObject
                    .TryGetComponent<IItem>(out var item))
                {
                    item.SecondaryUseItem();
                }
            }
            else if (!obj && _agent.PlayerInventory.IsSlotSelected() && !_agent.PlayerInventory.IsSelectedSlotEmpty())
            {
                if (_agent.PlayerInventory.GetInventorySlotObject().gameObject
                    .TryGetComponent<IItem>(out var item))
                {
                    item.SecondaryUseItem();
                }
            }
        }

        private void OnUse(bool obj)
        {
            if (obj && _agent.PlayerInventory.IsSlotSelected() && !_agent.PlayerInventory.IsSelectedSlotEmpty())
            {
                if (_agent.PlayerInventory.GetInventorySlotObject().gameObject
                    .TryGetComponent<IItem>(out var item))
                {
                    if (item.IsDisposable == false)
                        item.UseItem();
                    else
                    {
                        if (_agent.PlayerInventory.GetInventorySlotObject() is HealthKit healthKit)
                        {
                            if (_agent.Health.IsCurrentHealthMax == false)
                            {
                                SoundManager.Instance.PlaySFX("OpenBox", transform);
                                
                                item.UseItem();
                                _agent.PlayerInventory.ResetSlotItem();
                            }
                        }

                    }
                }
            }
        }
        
        private void OnGunLoad()
        {
            if(_agent.PlayerInventory.GetInventorySlotObject() is not Pistol) return;
            
            if (_agent.PlayerInventory.IsSlotSelected() && !_agent.PlayerInventory.IsSelectedSlotEmpty())
            {
                if (_agent.PlayerInventory.GetInventorySlotObject() is Pistol pistol)
                {
                    if (!pistol.IsAmmoFull() && _agent.PlayerAmmo.AmmoCount > 0)
                    {
                        SoundManager.Instance.PlaySFX("GunRifle", transform);
                        _agent.PlayerAmmo.AmmoCount = pistol.AddAmmo(_agent.PlayerAmmo.AmmoCount);
                    }
                }
            }
        }
    }
}