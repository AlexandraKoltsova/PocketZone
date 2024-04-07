namespace Services.Inventory
{
    public interface IInventorySystem : ISystem
    {
        public void EquipItem(int itemConfigID);
        public void UseItem(int itemConfigID);
        public void RemoveItem(int itemConfigID);
    }
}