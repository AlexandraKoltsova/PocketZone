using Services.Inventory;
using UI.Inventory;
using UnityEngine;

namespace Player
{
    public class PickUp : MonoBehaviour
    {
        private IInventorySystem _inventorySystem;

        public void Init(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DroppedItemView droppedItemView))
            {
                int value = _inventorySystem.AddItem(droppedItemView.ItemData, droppedItemView.ItemData.Amount);
                if (value == 0)
                    droppedItemView.DestroyItem();
                else
                    droppedItemView.ItemData.Amount = value;
            }
        }
    }
}