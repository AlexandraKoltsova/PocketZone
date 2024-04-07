using Data;

namespace Services.SaveLoad
{
    public interface ISaveLoadSystem : ISystem
    {
        public void Save();
        public void Load();
        public void SavePlayerProgress(PlayerProgress progress);
        public PlayerProgress LoadPlayerProgress();
    }
}