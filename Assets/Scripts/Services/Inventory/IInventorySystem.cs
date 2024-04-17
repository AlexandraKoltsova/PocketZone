using System;
using System.Collections.Generic;
using Logic.Inventory;

namespace Services.Inventory
{
    public interface IInventorySystem : ISystem
    {
        public void InformUpdateViewInventory();
        
        public event Action<Dictionary<int, ItemData>> OnInventoryUpdate;
        public event Action<ItemData> OnItemEquip;
        
        
        public int AddItem(ItemData itemData, int amount);
        public void EquipItem(int indexSlot, ItemData itemData);
        public void UseItem(int indexSlot, ItemData itemData);
        public void RemoveItem(int indexSlot);
    }
}