using System.Collections.Generic;
using Data;
using Data.Inventory;
using Logic.Inventory;
using Services.PersistentProgress;
using StaticData.Inventory;

namespace Services.Inventory
{
    public class InventorySystem : IInventorySystem, ISavedProgress
    {
        public List<ItemData> ListOfItemsData = new List<ItemData>();
        public List<ItemConfig> ListOfItemsConfig = new List<ItemConfig>();
        
        public void InitSystem()
        {
            foreach (ItemData itemData in ListOfItemsData)
            {
                //ListOfItemsData.Add(ItemData.GetEmptyItem());
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            // foreach (InventoryItemSaveData inventoryItem in  progress.InventoryData.InventoryItems)
            // {
            //     //_listOfItemsData.Add(inventoryItem);
            // }
        }

        public void SaveProgress(PlayerProgress progress)
        {
            
        }

        public void EquipItem(int itemConfigID)
        {
            
        }

        public void UseItem(int itemConfigID)
        {
            
        }

        public void RemoveItem(int itemConfigID)
        {
            
        }
    }
}