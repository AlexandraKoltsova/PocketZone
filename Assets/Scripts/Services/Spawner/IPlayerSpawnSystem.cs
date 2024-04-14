using UnityEngine;

namespace Services.Spawner
{
    public interface IPlayerSpawnSystem : ISystem
    {
        public void InitPlayer();
        public GameObject GetPlayer();
    }
}