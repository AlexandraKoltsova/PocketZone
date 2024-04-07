using System.Collections.Generic;
using System.Linq;
using StaticData;
using StaticData.MutantsData;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService 
    {
        private Dictionary<MutantTypeId, MutantStaticData> _mutants;
        private Dictionary<string, LevelStaticData> _levels;
        
        public void LoadMonsters()
        {
            _mutants = Resources.LoadAll<MutantStaticData>("StaticData/Mutants")
                .ToDictionary(x => x.MutantTypeId, x => x);
            
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MutantStaticData ForMutant(MutantTypeId TypeId)
        {
            return _mutants.TryGetValue(TypeId, out MutantStaticData staticData) ? staticData : null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            return _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;
        }
    }
}