using System;
using _01_Scripts.SO;
using Settings.InputSystem;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class Player : Entity.Entity
    {
        #region [PlayerSO]
        [field:Header("PlayerSO")]
        [field: SerializeField] public PlayerInputSO PlayerInputSo { get; set; }
        [field: SerializeField] public PlayerStatsSO PlayerStatsSo { get; set; }
        #endregion
        #region [Checkers]
        [field:Header("Checkers")]
        [field: SerializeField] public InteractableChecker InteractableChecker { get; set; }
        [field: SerializeField] public GroundChecker GroundChecker { get; set; }
        
        [field: SerializeField] public StaminaChecker StaminaChecker { get; set; }
        #endregion
        #region [PlayerActions]
        [field:Header("PlayerActions")]
        [field: SerializeField] public PlayerInventory PlayerInventory { get; set; }
        
        [field: SerializeField] public PlayerUI PlayerUI { get; set; }
        
        [field: SerializeField] public PlayerMovement PlayerMovement { get; set; }
        
        [field: SerializeField] public PlayerInteract PlayerInteract { get; set; }
        
        [field: SerializeField] public PlayerUse PlayerUse { get; set; }
        #endregion
        #region [PlayerTarfrom]
        [field:Header("PlayerTarfrom")]
        [field: SerializeField] public Transform CatchPoint { get; set; }
        [field: SerializeField] public Transform AimingPoint { get; set; }
        [field: SerializeField] public Transform HandPoint { get; set; }
        #endregion
        
        [field:Header("UI")]
        [field: SerializeField] public TextMeshProUGUI HealthText;
        [field: SerializeField] public TextMeshProUGUI StaminaText;
        [field: SerializeField] public TextMeshProUGUI MoneyText;
        [field: SerializeField] public TextMeshProUGUI AmmoText;
        [field: SerializeField] public TextMeshProUGUI ItemExplanationText;
        
        [field: SerializeField] public PlayerMoney PlayerMoney;
        [field: SerializeField] public PlayerAmmo PlayerAmmo;
        [field: SerializeField] public CinemachineCamera CinemaCamera { get; set; }
        public Action OnGameOver { get; set; }
        
        private void Awake()
        {
            Health.SetMaxHealth(PlayerStatsSo.MaxHealth);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Health.OnTakeDamage += TakeDamage;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Health.OnTakeDamage -= TakeDamage;
        }

        protected override void Death()
        {
            OnGameOver?.Invoke();
        }
        
        
        private void TakeDamage()
        {
            VolumeManager.Instance.Vignette.color.value = Color.red;
            VolumeManager.Instance.Vignette.intensity.value = 0.5f;

            int rand = SoundManager.Instance.GetRandomSFX(1, 3);
            SoundManager.Instance.PlaySFX("BodyPunch" + rand, transform );
            
            CameraManager.Instance.CameraHitShaking();

            StartCoroutine(VolumeManager.Instance.BaseVignette());
        }
        
        private void OnDestroy()
        {
            Death();
        }
    }
}