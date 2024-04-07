using System.Collections.Generic;
using Data;
using Logic.Inventory;
using Services.PersistentProgress;

namespace Services.Inventory
{
    public class InventorySystem : IInventorySystem, ISavedProgress
    {
        private List<ItemData> listOfItems = new List<ItemData>();

        private void Init()
        {
            
        }

        public void LoadProgress(PlayerProgress progress)
        {
            
        }

        public void SaveProgress(PlayerProgress progress)
        {
            
        }
    }
}