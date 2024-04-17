using Logic.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemEquipUIView : BaseItemView
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private GameObject _slot;
        
        public EquipType EquipType;
        
        public void Awake()
        {
            _slot.SetActive(false);
        }
        
        protected override void Redraw()
        {
            base.Redraw();

            if (ItemData.IsReserved)
            {
                _slot.SetActive(true);
                _sprite.sprite = ItemData.Sprite;
            }
            else
            {
                _slot.SetActive(false);
            }
        }
    }
}