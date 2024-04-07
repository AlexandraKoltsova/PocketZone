using UnityEngine;

namespace Services.Input
{
    public interface IInputSystem : ISystem
    {
        Vector2 Axis { get; }
        
        bool IsAttackButton();
        bool IsInventoryButton();
    }
}