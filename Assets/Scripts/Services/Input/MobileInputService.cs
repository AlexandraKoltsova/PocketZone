using UnityEngine;

namespace Services.Input
{
    public class MobileInputService : InputService
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