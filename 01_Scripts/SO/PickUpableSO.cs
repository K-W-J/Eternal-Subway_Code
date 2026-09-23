using UnityEngine;

namespace _01_Scripts.Interactables.PickUpable
{
    public enum ItemType
    {
        Noen = -1,
        
        Fuel,
        Resource,
        Valuable,
        
        Max
    }
    
    [CreateAssetMenu(fileName = "PickUpableSO", menuName = "SO/PickUpableSO", order = 0)]
    public class PickUpableSO : ScriptableObject
    {
        public Sprite ItemIcon;

        public ItemType ItemType;
        
        public int Fuel;
        
        public int SellPrice;
        public int PurchasePrice;
        
        public string ItemName;
        
        [TextArea] public string ItemExplanation;
    }
}