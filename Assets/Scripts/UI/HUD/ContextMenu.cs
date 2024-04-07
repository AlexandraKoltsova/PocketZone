using Logic.Inventory;
using Services;
using Services.Inventory;
using StaticData.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class ContextMenu : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Button _dropButton;
        [SerializeField] private TMP_Text _actionText;
        
        private ItemConfig _itemConfig;
        private IInventorySystem _inventorySystem;

        private void Awake()
        {
            _inventorySystem = AllServices.Container.Single<IInventorySystem>();
        }

        private void Start()
        {
            _actionButton.onClick.AddListener(OnActionButton);
            _dropButton.onClick.AddListener(OnDropButton);
        }

        public void Show(ItemConfig itemConfig)
        {
            gameObject.SetActive(true);
            _itemConfig = itemConfig;
            
            switch (_itemConfig.ItemType)
            {
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
            switch (_itemConfig.ItemType)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                    _inventorySystem.EquipItem(_itemConfig.ID);
                    break;
                case ItemType.Food:
                    _inventorySystem.UseItem(_itemConfig.ID);
                    break;
            }
            
            Hide();
        }

        private void OnDropButton()
        {
            _inventorySystem.RemoveItem(_itemConfig.ID);
            Hide();
        }
    }
}