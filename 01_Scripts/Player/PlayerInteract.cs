using _01_Scripts.Interactables.Catchable;
using _01_Scripts.Interactables.Item;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Level;
using _01_Scripts.Tutorial;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool IsInteracting { get; private set; }
        public bool IsCatching { get; private set; }
        
        [SerializeField] private Player _agent;
        
        private bool _isFirstClicking;
        private bool _isSecondClicking;
        
        private void OnEnable()
        {
            _agent.PlayerInputSo.OnInteractAction += OnInteract;
            _agent.PlayerInputSo.OnUseAction += OnCatch;
        }
        private void OnDisable()
        {
            _agent.PlayerInputSo.OnInteractAction -= OnInteract;
            _agent.PlayerInputSo.OnUseAction -= OnCatch;
        }
        
        private void Update()
        {
            Interact();
            
            if (_isFirstClicking && !_isSecondClicking)
                _isFirstClicking = false;
        }

        private void OnInteract(bool obj)
        {
            IsInteracting = obj;

            if (obj)
                _isFirstClicking = true;
        }
        
        private void OnCatch(bool obj)
        {
            IsInteracting = obj;
            IsCatching = obj;

            if (obj)
                _isFirstClicking = true;
        }

        private void Interact()
        {
            if (IsInteracting && _isFirstClicking)
            {
                if(_agent.InteractableChecker.InteractableObject == null) return;

                if (IsCatching && _agent.InteractableChecker.InteractableObject is Catchable catchable)
                {
                    if (catchable.IsKeepClicking)
                    {
                        _isSecondClicking = true;
                        catchable.Interact(_agent.CatchPoint.transform.position);
                    }
                    else if (!catchable.IsKeepClicking && !_isSecondClicking)
                    {

                    }
                }
                else if (_agent.InteractableChecker.InteractableObject is PickUpable pickUpable)
                {
                    if(IsCatching) return;
                    
                    if (pickUpable.IsKeepClicking)
                    {
                        _isSecondClicking = true;
                    }
                    else if (!pickUpable.IsKeepClicking && !_isSecondClicking)
                    {
                        if (_agent.PlayerInventory.InventoryCheck())
                        {
                            if(LevelManager.Instance.MapLevel == 0 && TutorialLevelGuide.Instance.tutorialNumber == 2 || TutorialLevelGuide.Instance.tutorialNumber == 6)
                                TutorialLevelGuide.Instance.TutorialNumberCount();
                                
                            PickUpableCheck(pickUpable);
                        }
                    }
                }
                else
                {
                    if(IsCatching) return;
                        
                    if (_agent.InteractableChecker.InteractableObject.IsKeepClicking)
                    {
                        _isSecondClicking = true;
                    }
                    else if (!_agent.InteractableChecker.InteractableObject.IsKeepClicking && !_isSecondClicking)
                    {
                        _agent.InteractableChecker.InteractableObject.Interact();
                    }
                }
            }
            else
            {
                _isSecondClicking = false;
                IsCatching = false;
            }
        }

        private void PickUpableCheck(PickUpable pickUpable)
        {
            if (pickUpable.IsSaleItem)
            {
                if(GameManager.Instance.Player.PlayerMoney.MoneyCount >= pickUpable.PickUpableSo.PurchasePrice)
                {
                    pickUpable.BuyItem();
                    _agent.PlayerMoney.DecreaseMoney(pickUpable.PickUpableSo.PurchasePrice);
                }
                else
                {
                    SoundManager.Instance.PlaySFX("Wrong", transform);
                }
            }
            else
            {
                if (pickUpable.TryGetComponent<IItem>(out var item))
                {
                    item.Player = _agent;

                    if (item.IsRightNow)
                    {
                        SoundManager.Instance.PlaySFX("ItemPickup", transform);
                        
                        item.Player = _agent;
                        item.UseItem();
                        return; 
                    }
                }
                
                _agent.PlayerInventory.PutInInventory(pickUpable);
                pickUpable.Interact();
            }
        }
    }
}