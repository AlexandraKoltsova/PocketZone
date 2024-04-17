using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemSlotUIView : BaseItemView
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private GameObject _slot;
        [SerializeField] private Button _button;

        public int ID;
        public event Action<ItemSlotUIView> OnClick; 
        
        public void Awake()
        {
            _slot.SetActive(false);
            _button.interactable = false;
            _button.onClick.AddListener(OnButtonClick);
        }
        
        protected override void Redraw()
        {
            base.Redraw();

            if (ItemData.IsReserved)
            {
                _slot.SetActive(true);
                _button.interactable = true;
                
                _sprite.sprite = ItemData.Sprite;
                _amountText.text = ItemData.Amount > 1 ? $"{ItemData.Amount}" : "";
            }
            else
            {
                _amountText.text = "";
                
                _slot.SetActive(false);
                _button.interactable = false;
            }
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(this);
        }
    }
}