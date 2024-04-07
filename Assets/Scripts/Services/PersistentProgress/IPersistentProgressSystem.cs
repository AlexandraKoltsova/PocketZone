using Data;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressSystem : ISystem
    {
        public PlayerProgress Progress { get; set; }
    }
}