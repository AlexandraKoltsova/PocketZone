using System.Collections.Generic;
using System.Linq;
using Logic.Inventory;
using Services;
using Services.Inventory;
using UI.Inventory;
using UnityEngine;

namespace UI.HUD
{
    public class InventoryPageView : MonoBehaviour
    {
        private List<ItemSlotUIView> _listOfItemsView = new List<ItemSlotUIView>();
        private IInventorySystem _inventorySystem;
        
        private void Awake()
        {
            _inventorySystem = AllServices.Container.Single<IInventorySystem>();
            _listOfItemsView = GetComponentsInChildren<ItemSlotUIView>().ToList();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void UpdateSlotView(ItemData itemData)
        {
            foreach (ItemSlotUIView itemView in _listOfItemsView)
            {
                itemView.SetData(itemData);
            }
        }
    }
}