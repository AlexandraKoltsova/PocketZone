using System;

namespace Data.Inventory
{
    [Serializable]
    public class InventoryItemSaveData
    {
        public int Id;
        public int Amount;
        public bool IsEquiped;
    }
}