using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Mutant;
using Services.SaveLoad;
using StaticData.MutantsData;
using UnityEngine;

namespace Logic.Spawners
{
    public class MutantSpawner : MonoBehaviour, ILoadSystem
    {
        [SerializeField] private bool _slain;
        public bool Slain => _slain;
        
        public MutantTypeId MutantTypeId;
        public string Id { get; set; }

        public string SaveKey { get; } = AssetsAddress.MutantSpawnersSaveKey;

        private IGameFactory _gameFactory;
        private MutantDeath _mutantDeath;
        private KillData _killData;
        
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _killData = new KillData();
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

        public SaveData GetSaveData()
        {
            var json = JsonUtility.ToJson(_killData);
            
            var data = new SaveData
            {
                Key = AssetsAddress.MutantSpawnersSaveKey,
                Json = json
            };

            return data;
        }

        public void LoadSaveData(SaveData saveData)
        {
            if (saveData == null || string.IsNullOrEmpty(saveData.Json)) return;

            var data = JsonUtility.FromJson<KillData>(saveData.Json);
            if (data == null) return;

            _killData = data;
            
            if (_killData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }
    }
}