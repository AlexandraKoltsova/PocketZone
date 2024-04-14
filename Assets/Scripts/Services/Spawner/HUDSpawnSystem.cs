using Infrastructure.Factory;
using Logic;
using UI.HUD;
using UnityEngine;

namespace Services.Spawner
{
    public class HUDSpawnSystem : IHUDSpawnSystem
    {
        private IGameFactory _gameFactory;
        private IPlayerSpawnSystem _playerSpawn;
        
        private GameObject _hudGameObject;
        
        public HUDSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _playerSpawn = SystemsManager.Get<IPlayerSpawnSystem>();
        }

        public void InitHUD()
        {
            _hudGameObject = _gameFactory.CreateHUD();

            HealthController healthController = _hudGameObject.GetComponent<HealthController>();
            healthController.Construct(_playerSpawn.GetPlayer().GetComponent<IHealth>());
            healthController.UpdateHealthBar();
        }
    }
}