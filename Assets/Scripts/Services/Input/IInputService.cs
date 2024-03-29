using UnityEngine;

namespace Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        
        bool IsAttackButton();
        bool IsInventoryButton();
    }
}