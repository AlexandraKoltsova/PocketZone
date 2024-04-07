using UnityEngine;

namespace Services.Randomizer
{
    public class RandomSystem : IRandomSystem
    {
        public Vector3 RandomZone(Vector3 position)
        {
            return new Vector3(position.x + Random.Range(-4, 4), position.y + Random.Range(-4, 4));
        }
    }
}