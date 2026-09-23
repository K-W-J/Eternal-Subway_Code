using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerMoney : MonoBehaviour
    {
        public int MoneyCount { get; private set; }
        
        public void AddMoney(int money)
        {
            MoneyCount += money;
        }
        
        public void DecreaseMoney(int money)
        {
            MoneyCount -= money;
        }
    }
}