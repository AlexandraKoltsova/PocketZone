using System.Collections.Generic;
using System.Linq;
using StaticData.Mutant;
using StaticData.Player;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataSystem : IStaticDataSystem 
    {
        private List<MutantStaticData> _mutants;
        private PlayerStaticData _player;
        
        public void LoadConfigs()
        {
            _mutants = Resources.LoadAll<MutantStaticData>("StaticData/Mutants").ToList();
            _player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerStaticData");
        }

        public MutantStaticData GetMutant(int index)
        {
            return _mutants[index];
        }
        
        public PlayerStaticData GetPlayer()
        {
            return _player;
        }
        
        public int MutantCount()
        {
            return _mutants.Count;
        }
    }
}