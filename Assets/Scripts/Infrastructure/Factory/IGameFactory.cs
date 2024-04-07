using System.Collections.Generic;
using Services;
using Services.PersistentProgress;
using StaticData.MutantsData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : ISystem
    {
        public GameObject CreatePlayer(GameObject at);
        public GameObject CreateHUD();
        public GameObject CreateMutant(MutantTypeId mutantTypeId, Transform transform);
        public void CreateSpawner(Vector3 at, string spawnerId, MutantTypeId mutantTypeId);

        public List<ISavedProgressReader> progressReaders { get; }
        public List<ISavedProgress> progressWriters { get; }

        public void Cleanup();
    }
}