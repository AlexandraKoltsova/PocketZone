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
        [SerializeField] private Transform _equipmentsHolder;
        [SerializeField] private ContextMenu _contextMenu;

        private List<ItemSlotUIView> _itemsInventoryView = new List<ItemSlotUIView>();
        private List<ItemEquipUIView> _itemsEquipView = new List<ItemEquipUIView>();

        private IInventorySystem _inventorySystem;
        
        private int _slotId = 0;
        
        public void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
            _contextMenu.Construct(_inventorySystem);
            
            _itemsInventoryView = _slotsHolder.GetComponentsInChildren<ItemSlotUIView>().ToList();
            _itemsEquipView = _equipmentsHolder.GetComponentsInChildren<ItemEquipUIView>().ToList();
            
            _inventorySystem.OnInventoryUpdate += UpdateInventoryUI;
            _inventorySystem.OnItemEquip += UpdateEquipUI;

            foreach (ItemSlotUIView itemSlotView in _itemsInventoryView)
            {
                itemSlotView.ID = _slotId;
                _slotId++;
                
                itemSlotView.OnClick += ShowContextMenu;
            }
        }

        public void Show()
        {
            _slotsHolder.gameObject.SetActive(true);
            _equipmentsHolder.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _slotsHolder.gameObject.SetActive(false);
            _equipmentsHolder.gameObject.SetActive(false);
            _contextMenu.Hide();
        }

        private void UpdateInventoryUI(Dictionary<int, ItemData> inventoryState)
        {
            foreach (var item in inventoryState)
            {
                int index = item.Key;
                ItemData itemData = item.Value;
                
                _itemsInventoryView[index].SetData(itemData);
            }
        }

        private void UpdateEquipUI(ItemData itemData)
        {
            ItemEquipUIView itemEquipUIView = _itemsEquipView.FirstOrDefault(d => d.EquipType == itemData.EquipType);
            if (itemEquipUIView == null) return;
            itemEquipUIView.SetData(itemData);
        }

        private void ShowContextMenu(ItemSlotUIView ItemSlotView)
        {
            _contextMenu.Show(ItemSlotView);
        }

        private void OnDestroy()
        {
            _inventorySystem.OnInventoryUpdate -= UpdateInventoryUI;
            _inventorySystem.OnItemEquip -= UpdateEquipUI;
            
            foreach (ItemSlotUIView itemSlotView in _itemsInventoryView)
                itemSlotView.OnClick -= ShowContextMenu;
        }
    }
}