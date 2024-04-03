using StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public MutantStaticData ForMutant(MutantTypeId TypeId);
        public LevelStaticData  ForLevel(string sceneKey);
        
        public void LoadMonsters();
    }
}