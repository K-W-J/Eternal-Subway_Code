using System;
using UnityEngine;

namespace _01_Scripts.Interactables.Subway
{
    public class SubwayFuel
    {
        public int MaxFuel { get; set; }
        public int CurrentFuel { get; set; }
        
        public Action OnEmptyFuel;
        public void SetMaxFuel(int maxHealth)
        {
            MaxFuel = maxHealth;
            CurrentFuel = MaxFuel;
        }
        
        public void TakeFuel(int amount)
        {
            if(MaxFuel > CurrentFuel)
                CurrentFuel += amount;

            if (MaxFuel < CurrentFuel)
                CurrentFuel = MaxFuel;
        }
    }
}