using System.Collections.Generic;
using Infrastructure.Factory;
using Logic;
using Mutant;
using Services.Randomizer;
using Services.StaticData;
using StaticData.Mutant;
using UI.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Services.Spawner
{
    public class MutantSpawnSystem : IMutantSpawnSystem
    {
        private IGameFactory _gameFactory;
        private IStaticDataSystem _staticData;
        private IRandomSystem _random;
        private IPlayerSpawnSystem _playerSpawn;
        
        private List<GameObject> _mutants = new List<GameObject>();
        private MutantStaticData _mutantConfig;
        private GameObject _mutantGameObject;
        
        private int _countMutants = 3;
        private int _currentCountMutants;
        private int _mutantId = 1;
        
        public MutantSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _random = SystemsManager.Get<IRandomSystem>();
            _playerSpawn = SystemsManager.Get<IPlayerSpawnSystem>();
        }

        public void InitSpawner()
        {
            GameObject spawner = _gameFactory.CreateSpawner(Vector3.zero);
            SpawnMutant(spawner);
        }
        
        private void SpawnMutant(GameObject spawner)
        {
            while (_currentCountMutants < _countMutants)
            {
                int index = _random.RandomIndex(_staticData.MutantCount() - 1);
                _mutantConfig = _staticData.GetMutant(index);
                
                Vector2 spawnPoint = _random.GetRandomPositionAroundPlayer(spawner.transform.position, 10, 6);
                _mutantGameObject = _gameFactory.CreateMutant(_mutantConfig, spawnPoint, spawner.transform);
                SetId();
                SetData();
                
                _mutants.Add(_mutantGameObject);
                _currentCountMutants++;
            }
        }

        private void SetData()
        {
            IHealth health = _mutantGameObject.GetComponent<IHealth>();
            health.Current = _mutantConfig.Hp;
            health.Max = _mutantConfig.Hp;
            
            _mutantGameObject.GetComponent<HealthController>().Construct(health);
            _mutantGameObject.GetComponent<AgentMoveToPlayer>().Construct(_playerSpawn.GetPlayer().transform);
            _mutantGameObject.GetComponent<NavMeshAgent>().speed = _mutantConfig.MoveSpeed;
            
            _mutantGameObject.GetComponent<MutantAttack>()
                .Construct(_mutantConfig.Damage, _mutantConfig.AttackCooldown, _mutantConfig.CLeavage, _mutantConfig.EffectiveDistance);
        }

        private void SetId()
        {
            _mutantGameObject.GetComponent<MutantCharacter>().MutantId = _mutantId;
            _mutantId++;
        }
    }
}