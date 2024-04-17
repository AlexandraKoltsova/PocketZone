using Infrastructure.Factory;
using Logic;
using Services.Inventory;
using StaticData.Player;
using UI.HUD;
using UnityEngine;
using ContextMenu = UI.HUD.ContextMenu;

namespace Services.Spawner
{
    public class HUDSpawnSystem : IHUDSpawnSystem
    {
        private IGameFactory _gameFactory;
        private IPlayerSpawnSystem _playerSpawn;
        private IInventorySystem _inventorySystem;
        
        private PlayerStaticData _playerConfig;
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
            
            ProjectileView projectileView = _hudGameObject.GetComponent<ProjectileView>();
            projectileView.Construct(_playerSpawn.GetPlayer().GetComponent<IProjectile>());
            projectileView.UpdateProjectile();
            
            InventoryView inventoryView = _hudGameObject.GetComponentInChildren<InventoryView>();
            inventoryView.Construct(_inventorySystem);
        }
    }
}