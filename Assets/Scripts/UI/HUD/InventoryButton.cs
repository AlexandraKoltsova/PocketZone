using UnityEngine;

namespace UI.HUD
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private InventoryPageView _inventoryPage;
        private bool _isDisable;

        protected void Start()
        {
            _inventoryPage.Hide();
            _isDisable = true;
        }

        public void OnClick()
        {
            if (_isDisable)
            {
                _inventoryPage.Show();
                _isDisable = false;
            }
            else
            {
                _inventoryPage.Hide();
                _isDisable = true;
            }
        }
        
        private void OnValidate()
        {
            if (_inventoryPage == null)
            {
                _inventoryPage = GetComponent<InventoryPageView>();
            }
        }
    }
}