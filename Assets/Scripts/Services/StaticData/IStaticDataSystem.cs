using StaticData.Mutant;
using StaticData.Player;

namespace Services.StaticData
{
    public interface IStaticDataSystem : ISystem
    {
        public void LoadConfigs();

        public MutantStaticData GetMutant(int index);
        public PlayerStaticData GetPlayer();

        public int MutantCount();
    }
}