using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Inventory;
using Infrastructure.AssetManagement;
using Logic.Inventory;
using Services.SaveLoad;
using Services.Spawner;
using Services.StaticData;
using StaticData.Inventory;
using UnityEngine;

namespace Services.Inventory
{
    public class InventorySystem : IInventorySystem, ILoadSystem
    {
        public string SaveKey { get; } = AssetsAddress.InventorySaveKey;
        
        private List<ItemData> _inventoryItemsData = new List<ItemData>(12);
        private List<ItemConfig> _itemConfigs = new List<ItemConfig>();
        private InventoryData _inventoryData;

        private InventoryItemsSaveData _inventoryItemsSaveData;
        private int _inventorySize = 12;

        private IHUDSpawnSystem _hudSpawn;
        private IStaticDataSystem _staticData;

        public event Action<Dictionary<int, ItemData>> OnInventoryUpdate;

        public InventorySystem()
        {
            _hudSpawn = SystemsManager.Get<IHUDSpawnSystem>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _inventoryData = new InventoryData();
            _itemConfigs = _staticData.GetItemConfigs();
        }

        public void InitInventory()
        {
            for (int i = 0; i < _inventorySize; i++)
            {
                ItemData itemData = new ItemData();
                itemData.SetEmpty();
                _inventoryItemsData.Add(itemData);
            }

            InformAboutChange();
        }

        private int AddItem(ItemConfig itemConfig, int amount)
        {
            if (itemConfig.IsStackable == false)
            {
                for (int i = 0; i < _inventoryItemsData.Count; i++)
                {
                    while (amount > 0 && IsInventorHasEmptySlot())
                    {
                        amount -= AddItemToFirstFreeSlot(itemConfig, 1);
                    }
                    InformAboutChange();
                    return amount;
                }
            }
            
            amount = AddStackableItem(itemConfig, amount);
            InformAboutChange();
            return amount;
        }

        private bool IsInventorHasEmptySlot()
        {
            return _inventoryItemsData.Any(item => !item.IsReserved);
        }

        private int AddItemToFirstFreeSlot(ItemConfig itemConfig, int amount)
        {
            ItemData itemData = _inventoryItemsData.FirstOrDefault(d => d.IsReserved == false);
            if (itemData == null) return 0;
            
            itemData.SetData(itemConfig);
            itemData.ChangeAmount(amount);
            return amount;
        }

        private int AddStackableItem(ItemConfig itemConfig, int amount)
        {
            for (int i = 0; i < _inventoryItemsData.Count; i++)
            {
                if (_inventoryItemsData[i].IsReserved) continue;

                if (_inventoryItemsData[i].Id == itemConfig.ID)
                {
                    _inventoryItemsData[i].ChangeAmount(amount);
                    InformAboutChange();
                    return 0;
                }
            }

            while (amount > 0 && IsInventorHasEmptySlot())
            {
                amount--;
                AddItemToFirstFreeSlot(itemConfig, amount);
            }
            return amount;
        }

        public int AddItem(ItemData itemData, int amount)
        {
            ItemConfig item = _itemConfigs.FirstOrDefault(d => d.ID == itemData.Id);
            if(item == null) return amount;
            
            int remains = AddItem(item, amount);
            return remains;
        }

        private Dictionary<int, ItemData> GetCurrentSlotState()
        {
            Dictionary<int, ItemData> returnValue = new Dictionary<int, ItemData>();

            for (int i = 0; i < _inventoryItemsData.Count; i++)
            {
                if (!_inventoryItemsData[i].IsReserved) continue;
                
                returnValue[i] = _inventoryItemsData[i];
            }
            return returnValue;
        }

        private void InformAboutChange()
        {
            OnInventoryUpdate?.Invoke(GetCurrentSlotState());
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

        public SaveData GetSaveData()
        {
            foreach (ItemData itemData in _inventoryItemsData)
            {
                var inventoryItemsSaveData = new InventoryItemsSaveData();
                inventoryItemsSaveData.Id = itemData.Id;
                inventoryItemsSaveData.Amount = itemData.Amount;
                inventoryItemsSaveData.IsReserved = itemData.IsReserved;
                
                _inventoryData.InventoryItems.Add(inventoryItemsSaveData);
            }
            
            var json = JsonUtility.ToJson(_inventoryData);
            
            var data = new SaveData
            {
                Key = AssetsAddress.InventorySaveKey,
                Json = json
            };
            
            return data;
        }

        public void LoadSaveData(SaveData saveData)
        {
            if (saveData == null || string.IsNullOrEmpty(saveData.Json)) return;

            var data = JsonUtility.FromJson<InventoryData>(saveData.Json);
            if (data == null) return;
            _inventoryData = data;

            foreach (var inventoryItems in _inventoryData.InventoryItems)
            {
                ItemConfig itemConfig = _itemConfigs.FirstOrDefault(d => d.ID == inventoryItems.Id);
                if(itemConfig == null) return;
                
                AddItem(itemConfig, inventoryItems.Amount);
            }
        }
    }
}