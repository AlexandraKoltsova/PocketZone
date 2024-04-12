using System.Collections.Generic;
using Logic.Inventory;
using StaticData.Inventory;

namespace Services.Inventory
{
    public class InventorySystem : IInventorySystem
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