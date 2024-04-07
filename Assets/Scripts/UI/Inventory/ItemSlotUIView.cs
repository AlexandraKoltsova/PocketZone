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
        [SerializeField] private Button _button;

        public event Action<ItemSlotUIView> OnClick; 
        
        public override void Init()
        {
            base.Init();
            Redraw();
            
            _button.onClick.AddListener(OnButtonClick);
        }
        
        protected override void Redraw()
        {
            base.Redraw();

            if (ItemData != null)
            {
                _sprite.sprite = ItemData.Sprite;
                _amountText.text = ItemData.Amount > 1 ? $"{ItemData.Amount}" : "";

                _button.interactable = true;
            }
            else
            {
                _sprite.sprite = null;
                _amountText.text = "";
                
                _button.interactable = false;
            }
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(this);
        }
    }
}