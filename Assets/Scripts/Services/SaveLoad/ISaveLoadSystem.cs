namespace Services.SaveLoad
{
    public interface ISaveLoadSystem : ISystem
    {
        public void Save();
        public void Load();
    }
}