using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Logic;
using Logic.Spawners;
using Mutant;
using Services.PersistentProgress;
using Services.Randomizer;
using Services.StaticData;
using StaticData;
using UI.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        
        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> progressWriters { get; } = new List<ISavedProgress>();
        
        private GameObject PlayerGameObject { get; set; }
        
        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
        }

        public void Cleanup()
        {
            progressReaders.Clear();
            progressWriters.Clear();
        }

        public GameObject CreatePlayer(GameObject at)
        {
            PlayerGameObject = InstantiateRegistered(AssetsAddress.PlayerPrefabPath, at.transform.position);
            return PlayerGameObject;
        }

        public GameObject CreateHUD()
        {
            return InstantiateRegistered(AssetsAddress.HUDPrefabPath);
        }

        public GameObject CreateMutant(MutantTypeId TypeId, Transform parent)
        {
            MutantStaticData mutantData = _staticData.ForMutant(TypeId);

            GameObject mutantDataPrefab = mutantData.Prefab;
            Vector3 parentPosition = _randomService.RandomZone(parent.position);
            GameObject mutant = Object.Instantiate(mutantDataPrefab, parentPosition, Quaternion.identity, parent);

            IHealth health = mutant.GetComponent<IHealth>();
            health.Current = mutantData.Hp;
            health.Max = mutantData.Hp;

            mutant.GetComponent<HealthController>().Construct(health);
            mutant.GetComponent<AgentMoveToPlayer>().Construct(PlayerGameObject.transform);
            mutant.GetComponent<NavMeshAgent>().speed = mutantData.MoveSpeed;

            MutantAttack attack = mutant.GetComponent<MutantAttack>();
            attack.Damage = mutantData.Damage;
            attack.AttackColldown = mutantData.AttackColldown;
            attack.CLeavage = mutantData.CLeavage;
            attack.EffectiveDistance = mutantData.EffectiveDistance;

            return mutant;
        }
        
        public void CreateSpawner(Vector3 at, string spawnerId, MutantTypeId mutantTypeId)
        {
            MutantSpawner spawner = InstantiateRegistered(AssetsAddress.SpawnerPrefabPath, at).GetComponent<MutantSpawner>();

            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MutantTypeId = mutantTypeId;
        }

        
        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                progressWriters.Add(progressWriter);
            }
            
            progressReaders.Add(progressReader);
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
    }
}