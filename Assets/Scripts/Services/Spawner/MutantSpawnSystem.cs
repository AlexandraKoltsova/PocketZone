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
        private ILootSpawnSystem _lootSpawn;

        private List<GameObject> _loots;
        private List<GameObject> _mutants = new List<GameObject>();
        private MutantStaticData _mutantConfig;
        
        private int _countMutants = 3;
        private int _mutantId = 1;
        
        public MutantSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _random = SystemsManager.Get<IRandomSystem>();
            _playerSpawn = SystemsManager.Get<IPlayerSpawnSystem>();
            _lootSpawn = SystemsManager.Get<ILootSpawnSystem>();
        }

        public void InitSpawner()
        {
            GameObject spawner = _gameFactory.CreateSpawner(Vector3.zero);
            SpawnMutant(spawner);
            _loots = _lootSpawn.GetLootGameObjects();
            
            foreach (GameObject mutant in _mutants)
            {
                mutant.GetComponent<MutantDeath>().Happened += RelocateLoot;
            }
        }

        private void SpawnMutant(GameObject spawner)
        {
            for (int i = 0; i < _countMutants; i++)
            {
                int index = _random.RandomIndex(_staticData.MutantsCount() - 1);
                _mutantConfig = _staticData.GetMutant(index);
                
                Vector2 spawnPoint = _random.GetRandomPositionAroundPlayer(spawner.transform.position, 11, 7);
                GameObject mutant = _gameFactory.CreateMutant(_mutantConfig, spawnPoint, spawner.transform);
                SetId(mutant);
                SetData(mutant);
                
                _mutants.Add(mutant);
            }
        }

        private void SetId(GameObject mutant)
        {
            mutant.GetComponent<MutantCharacter>().MutantId = _mutantId;
            _mutantId++;
        }

        private void SetData(GameObject mutant)
        {
            IHealth health = mutant.GetComponent<IHealth>();
            health.Current = _mutantConfig.Hp;
            health.Max = _mutantConfig.Hp;
            
            mutant.GetComponent<HealthController>().Construct(health);
            mutant.GetComponent<AgentMoveToPlayer>().Construct(_playerSpawn.GetPlayer().transform);
            mutant.GetComponent<NavMeshAgent>().speed = _mutantConfig.MoveSpeed;
            
            mutant.GetComponent<MutantAttack>()
                .Construct(_mutantConfig.Damage, _mutantConfig.AttackCooldown, _mutantConfig.CLeavage, _mutantConfig.EffectiveDistance);
        }
        
        private void RelocateLoot(Transform transform)
        {
            int index = _random.RandomIndex(_loots.Count - 1);
            
            _loots[index].transform.position = transform.position;
            _loots[index].SetActive(true);
        }
    }
}