using Logic.Inventory;
using UnityEngine;

namespace StaticData.Inventory
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "StaticData/Item")]
    public class ItemConfig : ScriptableObject
    {
        public ItemType ItemType;
        public EquipType EquipType;
        public int ID;
        public Sprite Image;
        public bool IsStackable;
    }
}