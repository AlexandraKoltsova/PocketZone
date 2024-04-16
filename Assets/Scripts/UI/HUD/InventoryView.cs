using System.Collections.Generic;
using System.Linq;
using Logic.Inventory;
using Services.Inventory;
using UI.Inventory;
using UnityEngine;

namespace UI.HUD
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsHolder;
        [SerializeField] private Transform _EquipmentsHolder;

        public List<ItemSlotUIView> ItemsView = new List<ItemSlotUIView>();

        private IInventorySystem _inventorySystem;

        public void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
            _inventorySystem.OnInventoryUpdate += UpdateInventoryUI;
        }
        
        private void Awake()
        {
            ItemsView = _slotsHolder.GetComponentsInChildren<ItemSlotUIView>().ToList();
        }

        public void Show()
        {
            _slotsHolder.gameObject.SetActive(true);
            _EquipmentsHolder.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _slotsHolder.gameObject.SetActive(false);
            _EquipmentsHolder.gameObject.SetActive(false);
        }
        
        private void UpdateInventoryUI(Dictionary<int, ItemData> inventoryState)
        {
            foreach (var item in inventoryState)
            {
                int index = item.Key;
                ItemData itemData = item.Value;
                
                ItemsView[index].SetData(itemData);
            }
        }

        private void OnDestroy()
        {
            _inventorySystem.OnInventoryUpdate -= UpdateInventoryUI;
        }
    }
}