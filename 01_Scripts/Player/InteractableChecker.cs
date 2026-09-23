using System;
using _01_Scripts.Level;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class InteractableChecker : MonoBehaviour
    {
        public Interactables.Interactable InteractableObject { get; private set; }
        public bool IsInteractable { get; private set; }

        [SerializeField] private Player _agent;

        [SerializeField] private Outline _outline;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            InteractableCheck();
        }

        private void OnDisable()
        {
            _outline.OnUnfocus();
        }

        public void InteractableCheck()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _agent.PlayerStatsSo.InteractionRange, ~0, QueryTriggerInteraction.Collide))
            {
                if (hit.transform.TryGetComponent<Interactables.Interactable>(out var interactable))
                {
                    if (InteractableObject != interactable && InteractableObject != null)
                    {
                        _outline.OnUnfocus();
                        InteractableObject = interactable;
                        _outline.OnFocus(InteractableObject.gameObject);
                    }
                    else if (InteractableObject == null)
                    {
                        InteractableObject = interactable;
                        _outline.OnFocus(InteractableObject.gameObject);
                        IsInteractable = true;
                    }

                    return;
                }
            }
            
            if (InteractableObject != null)
            {
                if (InteractableObject.IsKeepClicking && _agent.PlayerInteract.IsInteracting)
                {
                    return;
                }
                _outline.OnUnfocus();
            }
            
            InteractableObject = null;
            IsInteractable = false;
        }
    }
}
