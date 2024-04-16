using Infrastructure.AssetManagement;
using Services;
using StaticData.Mutant;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assets;
        
        private GameObject _playerGameObject;
        
        public GameFactory()
        {
            _assets = SystemsManager.Get<IAssetProvider>();
        }

        public GameObject CreatePlayer(GameObject at)
        {
            _playerGameObject = Instantiate(AssetsAddress.PlayerPrefabPath, at.transform.position);
            return _playerGameObject;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = Instantiate(AssetsAddress.HUDPrefabPath);
            return hud;
        }

        public GameObject CreateMutant(MutantStaticData mutantData, Vector2 spawnPoint, Transform parent)
        {
            GameObject mutantGameObject = Object.Instantiate(mutantData.Prefab, spawnPoint, Quaternion.identity, parent);
            return mutantGameObject;
        }

        public GameObject CreateSpawner(Vector2 at)
        {
            GameObject spawner = Instantiate(AssetsAddress.EmptyGameObjectPrefabPath, at);
            spawner.name = "MutantsHolder";
            return spawner;
        }
        
        public GameObject CreateLootHolder(Vector2 at)
        {
            GameObject collectible = Instantiate(AssetsAddress.EmptyGameObjectPrefabPath, at);
            collectible.name = "CollectibleHolder";
            return collectible;
        }
        
        public GameObject CreateLoot(Vector2 at, Transform parent)
        {
            GameObject loot = Instantiate(AssetsAddress.LootPrefabPath, at, parent);
            return loot;
        }
        
        private GameObject Instantiate(string prefabPath)
        {
            return _assets.Instantiate(prefabPath);
        }

        private GameObject Instantiate(string prefabPath, Vector2 at)
        {
            return _assets.Instantiate(prefabPath, at);
        }

        private GameObject Instantiate(string prefabPath, Vector2 at, Transform parent)
        {
            return _assets.Instantiate(prefabPath, at, parent);
        }
    }
}