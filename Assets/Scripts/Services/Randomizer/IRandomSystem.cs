using UnityEngine;

namespace Services.Randomizer
{
    public interface IRandomSystem : ISystem
    {
        public int RandomIndex(int maxIndex);

        public Vector2 GetRandomPositionAroundPlayer(Vector2 playerPosition, float spawnRadius, float safeDistance);
    }
}