using Data.PlayerStatus;
using Infrastructure.Factory;
using Services.StaticData;
using StaticData.Player;
using UnityEngine;

namespace Services.Spawner
{
    public class PlayerSpawnSystem : IPlayerSpawnSystem
    {
        private const string InitialPointTag = "InitialPoint";
        
        private IGameFactory _gameFactory;
        private IStaticDataSystem _staticData;
        
        private GameObject _playerGameObject;
        
        public PlayerSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
        }

        public void InitPlayer()
        {
            PlayerStaticData playerData = _staticData.GetPlayer();
            _playerGameObject = _gameFactory.CreatePlayer(playerData, at: GameObject.FindWithTag(InitialPointTag));
        }
        
        public Transform GetPlayerTransform()
        {
            return _playerGameObject.transform;
        }
    }
}