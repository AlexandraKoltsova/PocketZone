using UnityEngine;

namespace Services.Input
{
    public class MobileInputSystem : InputSystem
    {
        public override Vector2 Axis
        {
            get
            {
                return SimpleInputAxis();
            }
        }
    }
}