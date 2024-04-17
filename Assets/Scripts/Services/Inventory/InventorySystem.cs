using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Inventory;
using Infrastructure.AssetManagement;
using Logic.Inventory;
using Services.SaveLoad;
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

        private IStaticDataSystem _staticData;

        public event Action<Dictionary<int, ItemData>> OnInventoryUpdate;
        public event Action<ItemData> OnItemEquip;

        public InventorySystem()
        {
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _inventoryData = new InventoryData();
            _itemConfigs = _staticData.GetItemConfigs();

            InitInventory();
        }

        public void InformUpdateViewInventory()
        {
            OnInventoryUpdate?.Invoke(GetAllSlotState());
        }

        private void InitInventory()
        {
            for (int i = 0; i < _inventorySize; i++)
            {
                ItemData itemData = new ItemData();
                itemData.SetEmpty();
                _inventoryItemsData.Add(itemData);
            }
        }

        public void EquipItem(int indexSlot, ItemData itemData)
        {
            OnItemEquip?.Invoke(itemData);
            
            _inventoryItemsData[indexSlot].SetEmpty();
            InformUpdateViewInventory();
        }

        public void UseItem(int indexSlot, ItemData itemData)
        {
            Debug.Log("UseItem");
        }

        public void RemoveItem(int indexSlot)
        {
            _inventoryItemsData[indexSlot].SetEmpty();
            InformUpdateViewInventory();
        }

        public int AddItem(ItemData itemData, int amount)
        {
            ItemConfig item = _itemConfigs.FirstOrDefault(d => d.ID == itemData.Id);
            if (item == null) return amount;

            int remains = AddItem(item, amount);
            return remains;
        }

        private int AddItem(ItemConfig itemConfig, int amount)
        {
            if (itemConfig.IsStackable == false)
            {
                foreach (ItemData itemData in _inventoryItemsData)
                {
                    while (amount > 0 && IsInventorHasEmptySlot())
                    {
                        amount -= AddItemToFirstFreeSlot(itemConfig, 1);
                    }
                    
                    InformAboutChange();
                    return amount;
                }
            }
            else
            {
                amount = AddStackableItem(itemConfig, amount);
            }

            InformAboutChange();
            return amount;
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
            foreach (var itemData in _inventoryItemsData.Where(itemData => itemData.Id == itemConfig.ID))
            {
                itemData.ChangeAmount(amount);
                InformAboutChange();
                return 0;
            }

            while (amount > 0 && IsInventorHasEmptySlot())
            {
                amount--;
                AddItemToFirstFreeSlot(itemConfig, amount);
            }

            return amount;
        }

        private bool IsInventorHasEmptySlot()
        {
            return _inventoryItemsData.Any(item => !item.IsReserved);
        }

        private Dictionary<int, ItemData> GetAllSlotState()
        {
            Dictionary<int, ItemData> returnValue = new Dictionary<int, ItemData>();

            for (int i = 0; i < _inventoryItemsData.Count; i++)
                returnValue[i] = _inventoryItemsData[i];

            return returnValue;
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

            foreach (var inventoryItems in data.InventoryItems)
            {
                ItemConfig itemConfig = _itemConfigs.FirstOrDefault(d => d.ID == inventoryItems.Id);
                if (itemConfig == null) return;

                AddItem(itemConfig, inventoryItems.Amount);
            }

            InformUpdateViewInventory();
        }
    }
}