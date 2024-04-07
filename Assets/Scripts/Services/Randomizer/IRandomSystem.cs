using UnityEngine;

namespace Services.Randomizer
{
    public interface IRandomSystem : ISystem
    {
        public Vector3 RandomZone(Vector3 position);
    }
}