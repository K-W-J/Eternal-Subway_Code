using System;
using _01_Scripts.Level;
using _01_Scripts.UI;
using UnityEngine;

namespace _01_Scripts.Interactables.PickUpable
{
    public class PickUpable : Catchable.Catchable
    {
        [field:SerializeField] public PickUpableSO PickUpableSo { get; private set; }
        [SerializeField] private ParticleSystem _particleSystem;
        public GameObject MarkUI { get; set; }
        public bool IsSaleItem { get; private set; }
        public bool IsPickUp { get; private set; }
        
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            _colliders = GetComponentsInChildren<Collider>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelStarted += ClearMarkUI;
        }

        private void OnDisable()
        {
            if (LevelManager.Instance != null)
                LevelManager.Instance.OnLevelStarted -= ClearMarkUI;
        }
        public void SettingSaleItem()
        {
            IsSaleItem = true;
            
            foreach (var rigidbodie in _rigidbodies)
            {
                rigidbodie.isKinematic = true;
            }
        }

        private void ClearMarkUI()
        {
            Destroy(MarkUI);
            MarkUI = null;
        }


        public virtual void PickUpObject()
        {
            IsSaleItem = false;
            IsPickUp = true;
            
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }

            foreach (var rigidbodie in _rigidbodies)
            {
                rigidbodie.isKinematic = true;
            }
            
            _particleSystem.gameObject.SetActive(false);
        }
    
        public virtual void DropObject(Vector3 dropPos)
        {
            IsPickUp = false;
            gameObject.transform.position = dropPos;
            
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
            
            foreach (var rigidbodie in _rigidbodies)
            {
                rigidbodie.isKinematic = false;
            }
            
            _particleSystem.gameObject.SetActive(true);
        }

        public override void Interact()
        {
            PickUpObject();
        }
        
        public void BuyItem()
        {
            SoundManager.Instance.PlaySFX("BuyAndSell", transform);
            DestroyMarkUI();
            IsSaleItem = false;
        }

        public void DestroyMarkUI()
        {
            if(MarkUI != null)
                Destroy(MarkUI);
        }
        
        public void SetMarkUI(GameObject markUI, string markUIText = "")
        {
            if (MarkUI != null)
                DestroyMarkUI();

            LookAtPlayerUI lookAtMarkUI = markUI.GetComponent<LookAtPlayerUI>();
            
            lookAtMarkUI.Target = transform;
            lookAtMarkUI.SetText(markUIText);
            
            MarkUI = markUI;
        }

    }
}
