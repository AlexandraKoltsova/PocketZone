using System.Collections.Generic;
using UnityEngine;

namespace Services.Spawner
{
    public interface ILootSpawnSystem : ISystem
    {
        public void InitLootHolder();
        public List<GameObject> GetLootGameObjects();
    }
}