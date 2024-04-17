using Logic.Inventory;
using Services.Inventory;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class ContextMenu : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Button _dropButton;
        [SerializeField] private TMP_Text _actionText;
        
        private IInventorySystem _inventorySystem;
        
        private ItemData _itemData;
        private int _idSlot;

        public void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
            
            _actionButton.onClick.AddListener(OnActionButton);
            _dropButton.onClick.AddListener(OnDropButton);

            Hide();
        }

        public void Show(ItemSlotUIView itemSlotView)
        {
            gameObject.SetActive(true);

            transform.position = itemSlotView.transform.position;
            _idSlot = itemSlotView.ID;
            _itemData = itemSlotView.ItemData;
            
            switch (_itemData.ItemType)
            {
                case ItemType.None:
                    return;
                case ItemType.Weapon:
                case ItemType.Armor:
                    _actionText.text = "EQUIP";
                    break;
                case ItemType.Food:
                    _actionText.text = "USE";
                    break;
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnActionButton()
        {
            switch (_itemData.ItemType)
            {
                case ItemType.None:
                    return;
                case ItemType.Weapon:
                case ItemType.Armor:
                    _inventorySystem.EquipItem(_idSlot, _itemData);
                    break;
                case ItemType.Food:
                    _inventorySystem.UseItem(_idSlot, _itemData);
                    break;
            }
            
            Hide();
        }

        private void OnDropButton()
        {
            _inventorySystem.RemoveItem(_idSlot);
            Hide();
        }
    }
}