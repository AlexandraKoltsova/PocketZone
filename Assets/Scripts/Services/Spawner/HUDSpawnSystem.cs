using Infrastructure.Factory;
using Logic;
using Services.Inventory;
using UI.HUD;
using UnityEngine;

namespace Services.Spawner
{
    public class HUDSpawnSystem : IHUDSpawnSystem
    {
        private IGameFactory _gameFactory;
        private IPlayerSpawnSystem _playerSpawn;
        private IInventorySystem _inventorySystem;
        
        private GameObject _hudGameObject;
        
        public HUDSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _playerSpawn = SystemsManager.Get<IPlayerSpawnSystem>();
            _inventorySystem = SystemsManager.Get<IInventorySystem>();
        }

        public void InitHUD()
        {
            _hudGameObject = _gameFactory.CreateHUD();

            HealthController healthController = _hudGameObject.GetComponent<HealthController>();
            healthController.Construct(_playerSpawn.GetPlayer().GetComponent<IHealth>());
            healthController.UpdateHealthBar();
            
            InventoryView inventoryView = _hudGameObject.GetComponentInChildren<InventoryView>();
            inventoryView.Construct(_inventorySystem);
        }
    }
}