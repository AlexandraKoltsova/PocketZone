using Data;
using Infrastructure.Factory;
using Mutant;
using Services;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic.Spawners
{
    public class MutantSpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MutantTypeId _mutantTypeId;
        [SerializeField] private bool _slain;
        public bool Slain => _slain;

        private string _id;
        
        private IGameFactory _factory;
        private MutantDeath _mutantDeath;
        
        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        public void SaveProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            GameObject mutant = _factory.CreateMutant(_mutantTypeId, transform);
            
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