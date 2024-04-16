using System;
using System.Collections.Generic;

namespace Data.Inventory
{
    [Serializable]
    public class InventoryData
    {
         public List<InventoryItemsSaveData> InventoryItems = new List<InventoryItemsSaveData>();
    }
}