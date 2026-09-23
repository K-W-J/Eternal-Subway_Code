using System.Collections;
using UnityEngine;

namespace _01_Scripts.Player
{
    public class StaminaChecker : MonoBehaviour
    {
        public bool CanRun { get; private set; } = true;
        
        [SerializeField] private Player _agent;
        
        public float MaxStamina;
        public float CurrentStamina;
        private bool _isStop = true;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            MaxStamina = _agent.PlayerStatsSo.MaxStamina;
            CurrentStamina = MaxStamina;
        }

        private void Update()
        {
            StaminaCheck();
        }

        private void StaminaCheck()
        {
            if (_agent.PlayerMovement.IsRuning && _agent.PlayerMovement.Velocity.magnitude != 0)
            {
                if (CurrentStamina > 0)
                {
                    CurrentStamina -= 2 * Time.deltaTime;
                }
                else
                {
                    CanRun = false;
                }
            }
            else
            {
                if (MaxStamina > CurrentStamina)
                {
                    if (CurrentStamina <= 0)
                    {
                        StartCoroutine(StaminaCheckCoroutine());
                    }
                    else
                    {
                        CurrentStamina += 2 * Time.deltaTime;
                        CanRun = true;
                    }
                }
            }
        }

        private IEnumerator StaminaCheckCoroutine()
        {
            if (!_isStop) yield break;
            
            _isStop = false;
            
            yield return new WaitForSeconds(_agent.PlayerStatsSo.StaminaDaley);
            
            CurrentStamina += 5;
            CanRun = true;
            
            _isStop = true;
        }

    }
}