using Infrastructure.Factory;

namespace Services.Spawner
{
    public class HUDSpawnSystem : IHUDSpawnSystem
    {
        private IGameFactory _gameFactory;
        
        public HUDSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
        }

        public void InitHUD()
        {
            _gameFactory.CreateHUD();
        }
    }
}