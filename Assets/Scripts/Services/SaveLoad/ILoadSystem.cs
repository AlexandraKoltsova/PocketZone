using Data;

namespace Services.SaveLoad
{
    public interface ILoadSystem
    {
        public string SaveKey { get; }
        
        public SaveData GetSaveData();
        public void LoadSaveData(SaveData saveData);
    }
}