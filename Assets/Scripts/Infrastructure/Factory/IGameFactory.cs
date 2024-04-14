using Services;
using StaticData.Mutant;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : ISystem
    {
        public GameObject CreatePlayer(GameObject at);
        public GameObject CreateHUD();
        
        public GameObject CreateMutant(MutantStaticData mutantData, Vector2 spawnPoint, Transform parent);
        public GameObject CreateSpawner(Vector2 at);

        public GameObject CreateCollectibleHolder(Vector2 at);
        public GameObject CreateLoot(Vector2 at, Transform parent);
    }
}