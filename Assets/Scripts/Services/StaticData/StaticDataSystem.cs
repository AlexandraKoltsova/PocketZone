using System.Collections.Generic;
using System.Linq;
using StaticData.Inventory;
using StaticData.Mutant;
using StaticData.Player;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataSystem : IStaticDataSystem 
    {
        private List<MutantStaticData> _mutants;
        private List<ItemConfig> _items;
        private PlayerStaticData _player;
        
        public void LoadConfigs()
        {
            _mutants = Resources.LoadAll<MutantStaticData>("StaticData/Mutants").ToList();
            _items = Resources.LoadAll<ItemConfig>("StaticData/Items").ToList();
            _player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerStaticData");
        }

        public MutantStaticData GetMutant(int index)
        {
            return _mutants[index];
        }
        
        public int MutantsCount()
        {
            return _mutants.Count;
        }

        public List<ItemConfig> GetItemConfigs()
        {
            return _items;
        }

        public PlayerStaticData GetPlayer()
        {
            return _player;
        }
    }
}