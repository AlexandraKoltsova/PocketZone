using System.Collections.Generic;
using StaticData.Inventory;
using StaticData.Mutant;
using StaticData.Player;

namespace Services.StaticData
{
    public interface IStaticDataSystem : ISystem
    {
        public void LoadConfigs();

        public MutantStaticData GetMutant(int index);
        public int MutantsCount();

        public List<ItemConfig> GetItemConfigs();
        
        public PlayerStaticData GetPlayer();
    }
}