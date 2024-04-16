using System;
using System.Collections.Generic;
using Logic.Inventory;

namespace Services.Inventory
{
    public interface IInventorySystem : ISystem
    {
        public void InitInventory();
        public int AddItem(ItemData itemData, int amount);
        public event Action<Dictionary<int, ItemData>> OnInventoryUpdate;
        
        
        public void EquipItem(int itemConfigID);
        public void UseItem(int itemConfigID);
        public void RemoveItem(int itemConfigID);
    }
}