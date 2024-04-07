using System;
using System.Collections.Generic;

namespace Data.Inventory
{
    [Serializable]
    public class InventoryData
    {
         public List<InventoryItemSaveData> InventoryItems = new List<InventoryItemSaveData>();
    }
}