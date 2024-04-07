using StaticData.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class ItemData
    {
        public readonly int Id;
        public readonly Sprite Sprite;
        public readonly ItemType ItemType;
        public readonly EquipType EquipType;
        public int Amount { get; private set; }

        private readonly bool _isStackable;

        public ItemData(ItemConfig config)
        {
            Id = config.ID;
            Sprite = config.Image;
            _isStackable = config.IsStackable;
            ItemType = config.ItemType;
            EquipType = config.EquipType;

            Amount = 1;
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