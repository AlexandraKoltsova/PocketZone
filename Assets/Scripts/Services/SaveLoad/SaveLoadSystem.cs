using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Services.SaveLoad
{
    [Serializable]
    public class SaveSnapShot
    {
        public List<SaveData> SaveData = new List<SaveData>();
    }
    
    public class SaveLoadSystem : ISaveLoadSystem
    {
        private const string MainSaveKey = "MAIN_SAVE";
        private List<SaveData> _saveData = new List<SaveData>();
        
        public void Save()
        {
            var saveSnapShot = new SaveSnapShot();
            
            var loadSystems = SystemsManager.GetAll<ILoadSystem>();
            foreach (var system in loadSystems)
            {
                var newData = system.GetSaveData();
                
                saveSnapShot.SaveData.Add(newData);
            }
            
            var json = JsonUtility.ToJson(saveSnapShot);
            PlayerPrefs.SetString(MainSaveKey, json);
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(MainSaveKey)) return;

            var mainData = JsonUtility.FromJson<SaveSnapShot>(PlayerPrefs.GetString(MainSaveKey));
            if (mainData == null) return;
            
            var loadSystems = SystemsManager.GetAll<ILoadSystem>();
            foreach (var system in loadSystems)
            {
                var saveData = mainData.SaveData.FirstOrDefault(d => d.Key == system.SaveKey);
                if (saveData == null) continue;
                
                system.LoadSaveData(saveData);
            }
        }
    }
}