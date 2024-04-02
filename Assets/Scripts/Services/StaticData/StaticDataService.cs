using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService 
    {
        private Dictionary<MutantTypeId, MutantStaticData> _mutants;
        
        public void LoadMonsters()
        {
            _mutants = Resources.LoadAll<MutantStaticData>("StaticData/Mutants")
                .ToDictionary(x => x.MutantTypeId, x => x);
        }
        
        public MutantStaticData ForMutant(MutantTypeId TypeId)
        {
            return _mutants.TryGetValue(TypeId, out MutantStaticData staticData) ? staticData : null;
        }
    }
}