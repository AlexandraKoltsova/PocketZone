using StaticData.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class ItemData
    {
        public bool IsReserved;
        
        public int Id;
        public Sprite Sprite;
        public ItemType ItemType;
        public EquipType EquipType;
        public int Amount { get; set; }

        private bool _isStackable;

        public void SetData(ItemConfig config)
        {
            Id = config.ID;
            Sprite = config.Image;
            _isStackable = config.IsStackable;
            ItemType = config.ItemType;
            EquipType = config.EquipType;
            IsReserved = true;
            
            Amount = 1;
        }
        
        public void SetEmpty()
        {
            Id = 0;
            Amount = 0;
            Sprite = null;
            _isStackable = false;
            IsReserved = false;

            ItemType = ItemType.None;
            EquipType = EquipType.None;
        }

        public int ChangeAmount(int value)
        {
            if (!_isStackable) return Amount;

            Amount += value;
            if (Amount <= 0) Amount = 1;

            return Amount;
        }
    }
}