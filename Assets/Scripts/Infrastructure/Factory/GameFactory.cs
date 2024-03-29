using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> progressWriters { get; } = new List<ISavedProgress>();
        
        public event Action PlayerCreated;
        public GameObject PlayerGameObject { get; set; }
        
        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public void Cleanup()
        {
            progressReaders.Clear();
            progressWriters.Clear();
        }

        public GameObject CreatePlayer(GameObject at)
        {
            PlayerGameObject = InstantiateRegistered(AssetsAddress.PlayerPrefabPath, at.transform.position);
            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

        public void CreateHUD()
        {
            InstantiateRegistered(AssetsAddress.HUDPrefabPath);
        }
        
        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
        
        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                progressWriters.Add(progressWriter);
            }
            
            progressReaders.Add(progressReader);
        }
    }
}