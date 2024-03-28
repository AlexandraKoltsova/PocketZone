using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string ShootButton = "Shoot";
        private const string InventoryButton = "Inventory";
        
        public abstract Vector2 Axis { get; }
        
        public bool IsAttackButton()
        {
            return SimpleInput.GetButtonUp(ShootButton);
        }
        
        public bool IsInventoryButton()
        {
            return SimpleInput.GetButtonUp(InventoryButton);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}