using StaticData;
using StaticData.MutantsData;

namespace Services.StaticData
{
    public interface IStaticDataSystem : ISystem
    {
        public MutantStaticData ForMutant(MutantTypeId TypeId);
        public LevelStaticData  ForLevel(string sceneKey);
        
        public void LoadMonsters();
    }
}