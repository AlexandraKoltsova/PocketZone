using Data;
using Infrastructure.Factory;
using Mutant;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic.Spawners
{
    public class MutantSpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private bool _slain;
        public bool Slain => _slain;
        
        public MutantTypeId MutantTypeId;
        public string Id { get; set; }
        
        private IGameFactory _gameFactory;
        private MutantDeath _mutantDeath;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void SaveProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject mutant = _gameFactory.CreateMutant(MutantTypeId, transform);
            
            _mutantDeath = mutant.GetComponent<MutantDeath>();
            _mutantDeath.Happened += Slay;
        }
        
        private void Slay()
        {
            if (_mutantDeath != null)
                _mutantDeath.Happened -= Slay;

            _slain = true;
        }
    }
}