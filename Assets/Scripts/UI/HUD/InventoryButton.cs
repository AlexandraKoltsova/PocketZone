using UnityEngine;

namespace UI.HUD
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        private bool _isDisable;

        protected void Start()
        {
            _inventoryView.Show();
            _inventoryView.Hide();
            _isDisable = true;
        }

        public void OnClick()
        {
            if (_isDisable)
            {
                _inventoryView.Show();
                _isDisable = false;
            }
            else
            {
                _inventoryView.Hide();
                _isDisable = true;
            }
        }
        
        private void OnValidate()
        {
            if (_inventoryView == null)
            {
                _inventoryView = GetComponent<InventoryView>();
            }
        }
    }
}