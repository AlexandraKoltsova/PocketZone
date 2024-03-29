using System;
using System.Collections.Generic;
using Services;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreatePlayer(GameObject at);
        public void CreateHUD();
        
        public List<ISavedProgressReader> progressReaders { get; }
        public List<ISavedProgress> progressWriters { get; }
        
        public void Cleanup();
        
        public event Action PlayerCreated;
        public GameObject PlayerGameObject { get; }
    }
}