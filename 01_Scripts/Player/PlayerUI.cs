using _01_Scripts.Interactables.Item.Gun;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Player _agent;
        [SerializeField] private GameObject EseObject;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            _agent.PlayerInputSo.On1KeyAction += OnInventory;
            _agent.PlayerInputSo.On2KeyAction += OnInventory;
            _agent.PlayerInputSo.On3KeyAction += OnInventory;
            _agent.PlayerInputSo.On4KeyAction += OnInventory;
            _agent.PlayerInputSo.OnEsc += Ese;
        }

        public void Ese()
        {
            if (!EseObject.gameObject.activeSelf)
            {
                EseObject.gameObject.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
            }
            else
            {
                EseObject.gameObject.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }

        private void OnDisable()
        {
            _agent.PlayerInputSo.On1KeyAction -= OnInventory;
            _agent.PlayerInputSo.On2KeyAction -= OnInventory;
            _agent.PlayerInputSo.On3KeyAction -= OnInventory;
            _agent.PlayerInputSo.On4KeyAction -= OnInventory;
            _agent.PlayerInputSo.OnEsc -= Ese;
        }

        private void Update()
        {
            _agent.HealthText.text = $"{_agent.Health.CurrentHealth.ToString()} / {_agent.Health.MaxHealth.ToString()}";
            _agent.StaminaText.text = $"{((int)_agent.StaminaChecker.CurrentStamina).ToString()} / {((int)_agent.StaminaChecker.MaxStamina).ToString()}";
            _agent.MoneyText.text = _agent.PlayerMoney.MoneyCount + "$";

            if (_agent.PlayerInventory.GetSoltItem() is Pistol pistol)
            {
                _agent.AmmoText.gameObject.SetActive(true);
                _agent.AmmoText.text = pistol.CurrentAmmo + "/" + _agent.PlayerAmmo.AmmoCount;
            }
            else
            {
                _agent.AmmoText.gameObject.SetActive(false);
            }

            if (_agent.PlayerInventory.GetSoltItem() != null)
                _agent.ItemExplanationText.text = _agent.PlayerInventory.GetSoltItem().PickUpableSo.ItemExplanation;
            else
                _agent.ItemExplanationText.text = "";
        }

        private void OnInventory(int cellNumder)
        {
            _agent.PlayerInventory.SelectSolt(cellNumder);
        }
    }
}