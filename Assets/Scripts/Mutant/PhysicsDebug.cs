using UnityEngine;

namespace Mutant
{
    public static class PhysicsDebug
    {
        public static void DrawDebug(UnityEngine.Vector2 worldPos, float radius, float seconds)
        {
            Debug.DrawRay(worldPos, radius * Vector2.up, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector2.down, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector2.left, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector2.right, Color.red, seconds);
        }
    }
}