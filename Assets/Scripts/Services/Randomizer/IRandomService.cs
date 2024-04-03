using UnityEngine;

namespace Services.Randomizer
{
    public interface IRandomService : IService
    {
        public Vector3 RandomZone(Vector3 position);
    }
}