using System.Collections.Generic;
using _01_Scripts.Interactables.PickUpable;
using _01_Scripts.Level;
using _01_Scripts.NPC;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Store
{
    public class Store : MonoBehaviour
    {
        [field:SerializeField] public List<GameObject> ItemStands { get; set; } = new List<GameObject>();
        [field:SerializeField] public List<GameObject> Items { get; set; } = new List<GameObject>();

        [SerializeField] private GameObject npc;

        [SerializeField] private GameObject priceUI;
        [SerializeField] private GameObject worldCanvas;

        private Npc _storeman;

        private void Awake()
        {
            _storeman = npc.GetComponent<Npc>();
        }

        private void OnEnable()
        {
            if(LevelManager.Instance == null) return;
            
            LevelManager.Instance.OnLevelCleared += SettingStore;
            LevelManager.Instance.OnLevelStarted += ClearStore;
            _storeman.OnDeathNpc += NpcDeath;
        }
        
        private void OnDisable()
        {
            if(LevelManager.Instance == null) return;
            
            LevelManager.Instance.OnLevelCleared -= SettingStore;
            LevelManager.Instance.OnLevelStarted -= ClearStore;
            _storeman.OnDeathNpc -= NpcDeath;
        }
        
        public void PriceUISetting(PickUpable item, bool isSaleItem)
        {
            GameObject lookAtPlayerUI = Instantiate(priceUI, worldCanvas.transform);
            
            if(isSaleItem)
                item.SetMarkUI(lookAtPlayerUI, item.PickUpableSo.PurchasePrice + "$");
            else
                item.SetMarkUI(lookAtPlayerUI, item.PickUpableSo.SellPrice + "$");
        }


        private void NpcDeath()
        {
            foreach (var itemStand in ItemStands)
            {
                ItemStand stand = itemStand.GetComponent<ItemStand>();
                
                if (!stand.GetComponent<ItemStand>().ItemStandCheck())
                {
                    stand.GetComponent<ItemStand>().BuyItemSale();
                }
            }
            
            SoundManager.Instance.StopBGM("StoreBGM");
        }

        private void SettingStore()
        {
            if (npc == null)
            {
                if(LevelManager.Instance == null) return;
                
                LevelManager.Instance.OnLevelCleared -= SettingStore;
                LevelManager.Instance.OnLevelStarted -= ClearStore;
                return;
            }
            
            SoundManager.Instance.PlayBGM("StoreBGM", transform);
            
            transform.GetChild(0).gameObject.SetActive(true);
            
            for (int i = 0; i < ItemStands.Count; i++)
            {
                int rand = Random.Range(0, Items.Count);
                GameObject itemObject = Instantiate(Items[rand].gameObject);
                PickUpable item = itemObject.GetComponentInChildren<PickUpable>();

                item.SettingSaleItem();
                PriceUISetting(item, true);
                
                ItemStands[i].GetComponent<ItemStand>().SettingItemStand(item);
            }
        }
        private void ClearStore()
        {
            SoundManager.Instance.StopBGM("StoreBGM");
            
            for (int i = 0; i < ItemStands.Count; i++)
            {
                ItemStand stand = ItemStands[i].GetComponent<ItemStand>();
                
                if(!stand.ItemStandCheck())
                    stand.DestroyItem();
            }
            
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}