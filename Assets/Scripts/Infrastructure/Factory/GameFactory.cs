using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Logic;
using Logic.Spawners;
using Mutant;
using Player;
using Services;
using Services.PersistentProgress;
using Services.Randomizer;
using Services.StaticData;
using StaticData;
using StaticData.MutantsData;
using UI.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assets;
        private IStaticDataSystem _staticData;
        private IRandomSystem _randomSystem;
        
        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> progressWriters { get; } = new List<ISavedProgress>();

        private GameObject _player;
        private int _mutantId = 1;

        public virtual void InitSystem()
        {
            _assets = SystemsManager.Get<IAssetProvider>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _randomSystem = SystemsManager.Get<IRandomSystem>();
        }
        
        public void Cleanup()
        {
            progressReaders.Clear();
            progressWriters.Clear();
        }

        public GameObject CreatePlayer(GameObject at)
        {
            _player = InstantiateRegistered(AssetsAddress.PlayerPrefabPath, at.transform.position);
            return _player;
        }

        public GameObject CreateHUD()
        {
            return InstantiateRegistered(AssetsAddress.HUDPrefabPath);
        }

        public GameObject CreateMutant(MutantTypeId TypeId, Transform parent)
        {
            MutantStaticData mutantData = _staticData.ForMutant(TypeId);

            GameObject mutantDataPrefab = mutantData.Prefab;
            Vector3 parentPosition = _randomSystem.RandomZone(parent.position);
            GameObject mutant = Object.Instantiate(mutantDataPrefab, parentPosition, Quaternion.identity, parent);

            MutantCharacter mutantCharacter = mutant.GetComponent<MutantCharacter>();
            mutantCharacter.MutantId = _mutantId;
            _mutantId++;
            
            IHealth health = mutant.GetComponent<IHealth>();
            health.Current = mutantData.Hp;
            health.Max = mutantData.Hp;

            mutant.GetComponent<HealthController>().Construct(health);
            mutant.GetComponent<AgentMoveToPlayer>().Construct(_player.transform);
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