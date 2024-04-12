using System.Collections.Generic;
using Infrastructure.Factory;
using Services.Randomizer;
using Services.StaticData;
using StaticData.Mutant;
using UnityEngine;

namespace Services.Spawner
{
    public class MutantSpawnSystem : IMutantSpawnSystem
    {
        private IGameFactory _gameFactory;
        private IStaticDataSystem _staticData;
        private IRandomSystem _randomSystem;

        private List<GameObject> _mutants = new List<GameObject>();
        
        private int _countMutants = 3;
        private int _currentCountMutants;

        public MutantSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _randomSystem = SystemsManager.Get<IRandomSystem>();
        }

        public void InitSpawner()
        {
            _gameFactory.CreateSpawner(Vector3.zero);
            SpawnMutant();
        }
        
        public void SpawnMutant()
        {
            while (_currentCountMutants < _countMutants)
            {
                int index = _randomSystem.RandomIndex(_staticData.MutantCount() - 1);
                MutantStaticData mutantStaticData = _staticData.GetMutant(index);
                
                GameObject mutant = _gameFactory.CreateMutant(mutantStaticData);
                
                _mutants.Add(mutant);
                _currentCountMutants++;
            }
        }
    }
}