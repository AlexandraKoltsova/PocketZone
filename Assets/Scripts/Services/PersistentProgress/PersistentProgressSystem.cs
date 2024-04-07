using Data;

namespace Services.PersistentProgress
{
    public class PersistentProgressSystem : IPersistentProgressSystem
    {
        public PlayerProgress Progress { get; set; }
    }
}