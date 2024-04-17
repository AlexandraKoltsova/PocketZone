using Data;
using Data.Player;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Player;
using Services.SaveLoad;
using Services.StaticData;
using StaticData.Player;
using UnityEngine;

namespace Services.Spawner
{
    public class PlayerSpawnSystem : IPlayerSpawnSystem, ILoadSystem
    {
        private const string InitialPointTag = "InitialPoint";
        public string SaveKey { get; } = AssetsAddress.PlayerStatsSaveKey;
        
        private IGameFactory _gameFactory;
        private IStaticDataSystem _staticData;
        private PlayerStats _playerData;
        private PlayerStaticData _playerConfig;
        
        private GameObject _playerGameObject;
        private PlayerHealth _healthPlayer;
        private PlayerMovement _movementPlayer;
        private AimZone _aimPlayer;
        private PlayerShooting _shootingPlayer;
        
        public PlayerSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
            _playerData = new PlayerStats();
            _playerConfig = _staticData.GetPlayer();
            
            _playerData.CurrentHP = _playerConfig.MaxHP;
            _playerData.MaxHP = _playerConfig.MaxHP;
            _playerData.MoveSpeed = _playerConfig.MoveSpeed;
            _playerData.AimRadius = _playerConfig.AimRadius;
            
            _playerData.Damage = _playerConfig.Damage;
            _playerData.BulletSpeed = _playerConfig.BulletSpeed;
            _playerData.BulletCurrent = _playerConfig.BulletMaxCount;
            _playerData.BulletMax = _playerConfig.BulletMaxCount;
        }

        public void InitPlayer()
        {
            _playerGameObject = _gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            SetData();
        }

        private void SetData()
        {
            _healthPlayer = _playerGameObject.GetComponent<PlayerHealth>();
            _healthPlayer.Max = _playerData.MaxHP;
            _healthPlayer.Current = _playerData.CurrentHP;
            
            _movementPlayer = _playerGameObject.GetComponent<PlayerMovement>();
            _movementPlayer.MoveSpeed = _playerData.MoveSpeed;
            
            _aimPlayer = _playerGameObject.GetComponent<AimZone>();
            _aimPlayer.AimRadius = _playerData.AimRadius;
            
            _shootingPlayer = _playerGameObject.GetComponent<PlayerShooting>();
            _shootingPlayer.Damage = _playerData.Damage;
            _shootingPlayer.BulletSpeed = _playerData.BulletSpeed;
            _shootingPlayer.Current = _playerData.BulletCurrent;
            _shootingPlayer.Max = _playerData.BulletMax;
        }

        public GameObject GetPlayer()
        {
            return _playerGameObject;
        }

        public SaveData GetSaveData()
        {
            _playerData.CurrentHP = _healthPlayer.Current;
            _playerData.MaxHP = _healthPlayer.Max;
            _playerData.MoveSpeed = _movementPlayer.MoveSpeed;
            _playerData.AimRadius = _aimPlayer.AimRadius;
            _playerData.Damage = _shootingPlayer.Damage;
            _playerData.BulletSpeed = _shootingPlayer.BulletSpeed;
            _playerData.BulletCurrent = _shootingPlayer.Current;
            _playerData.BulletMax = _shootingPlayer.Max;
            
            var json = JsonUtility.ToJson(_playerData);
            
            var data = new SaveData
            {
                Key = AssetsAddress.PlayerStatsSaveKey,
                Json = json
            };
            
            return data;
        }

        public void LoadSaveData(SaveData saveData)
        {
            if (saveData == null || string.IsNullOrEmpty(saveData.Json)) return;

            var data = JsonUtility.FromJson<PlayerStats>(saveData.Json);
            if (data == null) return;

            _playerData.CurrentHP = data.CurrentHP;
            _playerData.MaxHP = data.MaxHP;
            _playerData.MoveSpeed = data.MoveSpeed;
            _playerData.AimRadius = data.AimRadius;
            _playerData.Damage = data.Damage;
            _playerData.BulletSpeed = data.BulletSpeed;
            _playerData.BulletCurrent = data.BulletCurrent;
            _playerData.BulletMax = data.BulletMax;
        }
    }
}