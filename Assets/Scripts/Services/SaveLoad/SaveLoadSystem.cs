using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Factory;
using Services.PersistentProgress;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadSystem : ISaveLoadSystem
    {
        private const string ProgressKey = "Progress";
        private const string MainSaveKey = "MAIN_SAVE";
        
        private IPersistentProgressSystem _progressSystem;
        private IGameFactory _gameFactory;

        private List<SaveData> _saveData = new List<SaveData>();

        public virtual void InitSystem()
        {
            _progressSystem = SystemsManager.Get<IPersistentProgressSystem>();
            _gameFactory = SystemsManager.Get<IGameFactory>();
        }
        
        public void Save()
        {
            var loadSystems = SystemsManager.GetAll<ILoadSystem>();
            foreach (var system in loadSystems)
            {
                var newData = system.GetSaveData();
                
                var existData = _saveData.FirstOrDefault(d => d.Key == system.SaveKey);
                if (existData == null)
                {
                    _saveData.Add(newData);
                }
                else
                {
                    existData.Json = newData.Json;
                }
            }

            var json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString(MainSaveKey, json);
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(MainSaveKey)) return;

            var mainData = JsonUtility.FromJson<List<SaveData>>(PlayerPrefs.GetString(MainSaveKey));
            if (mainData == null) return;
            _saveData = mainData;

            var loadSystems = SystemsManager.GetAll<ILoadSystem>();
            foreach (var system in loadSystems)
            {
                var saveData = _saveData.FirstOrDefault(d => d.Key == system.SaveKey);
                if (saveData == null) continue;
                
                system.LoadSaveData(saveData);
            }
        }

        public void SavePlayerProgress(PlayerProgress playerProgress)
        {
            var json = JsonUtility.ToJson(playerProgress);
            
            var saveData = _saveData.FirstOrDefault(d => d.Key == ProgressKey);

            if (saveData != null)
            {
                saveData.Json = json;
            }
            else
            {
                saveData = new SaveData
                {
                    Key = ProgressKey,
                    Json = json
                };
                
                _saveData.Add(saveData);
            }
        }
        
        public PlayerProgress LoadPlayerProgress()
        {
            var saveData = _saveData.FirstOrDefault(d => d.Key == ProgressKey);
            if (saveData == null) return null;
            
            var data = JsonUtility.FromJson<PlayerProgress>(saveData.Json);
            return data;
        }
    }
}