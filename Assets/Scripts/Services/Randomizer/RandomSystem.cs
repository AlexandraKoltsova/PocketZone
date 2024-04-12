using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.Randomizer
{
    public class RandomSystem : IRandomSystem
    {
        public int RandomIndex(int maxIndex)
        {
            return Random.Range(0, maxIndex + 1);
        }
        
        public Vector2 GetRandomPositionAroundPlayer(Vector2 playerPosition, float spawnRadius, float safeDistance)
        {
            if (spawnRadius <= safeDistance) throw new Exception("Spawn radius can't be lower safe distance");

            var randomPoint = playerPosition + Random.insideUnitCircle * spawnRadius;

            if (InsideArea(randomPoint, playerPosition, safeDistance)) 
                return GetRandomPositionAroundPlayer(playerPosition, spawnRadius, safeDistance);

            return randomPoint;
        }

        private bool InsideArea(Vector2 point, Vector2 playerPosition, float distanceAroundPlayer)
        {
            if (point.x < playerPosition.x + distanceAroundPlayer && point.y < playerPosition.y + distanceAroundPlayer)
            {
                if (point.x > playerPosition.x - distanceAroundPlayer && point.y > playerPosition.y - distanceAroundPlayer) return true;
            }
            return false;
        }
    }
}