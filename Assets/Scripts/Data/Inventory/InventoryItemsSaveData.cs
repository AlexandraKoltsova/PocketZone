using System;

namespace Data.Inventory
{
    [Serializable]
    public class InventoryItemsSaveData
    {
        public int Id;
        public int Amount;
        public bool IsReserved;
    }
}