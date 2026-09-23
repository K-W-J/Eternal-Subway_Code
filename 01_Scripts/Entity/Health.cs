using System;
using UnityEngine;

namespace _01_Scripts.Entity
{
    public class Health
    {
        public Action OnDeath;
        public Action OnTakeDamage;
        
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsCurrentHealthMax { get; private set; }
        
        public void SetMaxHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            IsCurrentHealthMax = true;
        }
        
        public void TakeDamage(int damage)
        {
            IsCurrentHealthMax = false;
            
            if (CurrentHealth > 0)
            {
                CurrentHealth -= damage;
                OnTakeDamage?.Invoke();
            }

            if(CurrentHealth <= 0)
                OnDeath?.Invoke();
        }
        
        public void HealHealth(int heel)
        {
            CurrentHealth += heel;
            
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;

            if (CurrentHealth == MaxHealth)
                IsCurrentHealthMax = true;
        }
    }
}