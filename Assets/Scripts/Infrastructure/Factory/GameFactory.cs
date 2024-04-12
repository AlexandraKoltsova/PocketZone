using Infrastructure.AssetManagement;
using Logic;
using Mutant;
using Player;
using Services;
using Services.Randomizer;
using StaticData.Mutant;
using StaticData.Player;
using UI.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assets;
        private IRandomSystem _randomSystem;
        
        private GameObject _spawnerGameObject;
        private GameObject _playerGameObject;
        private int _mutantId = 1;
        
        public GameFactory()
        {
            _assets = SystemsManager.Get<IAssetProvider>();
            _randomSystem = SystemsManager.Get<IRandomSystem>();
        }

        public GameObject CreatePlayer(PlayerStaticData playerData, GameObject at)
        {
            _playerGameObject = Instantiate(AssetsAddress.PlayerPrefabPath, at.transform.position);
            
            PlayerCharacter player = _playerGameObject.GetComponent<PlayerCharacter>();

            _playerGameObject.GetComponent<PlayerHealth>().Current = playerData.MaxHP;
            _playerGameObject.GetComponent<PlayerHealth>().Max = playerData.MaxHP;
            _playerGameObject.GetComponent<PlayerMovement>().Construct(playerData.MoveSpeed);
            _playerGameObject.GetComponent<AimZone>().Construct(playerData.AimRadius);
            
            player.ConstructAttack(playerData.Damage, playerData.BulletSpeed);
            
            return _playerGameObject;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = Instantiate(AssetsAddress.HUDPrefabPath);
            hud.GetComponent<HealthController>().Construct(_playerGameObject.GetComponent<IHealth>());
            
            return hud;
        }

        public GameObject CreateMutant(MutantStaticData mutantData)
        {
            Vector2 spawnPoint = _randomSystem.GetRandomPositionAroundPlayer(_spawnerGameObject.transform.position, 10, 6);
            GameObject mutantGameObject = Object.Instantiate(mutantData.Prefab, spawnPoint, Quaternion.identity, _spawnerGameObject.transform);
            
            mutantGameObject.GetComponent<MutantCharacter>().MutantId = _mutantId;
            _mutantId++;
            
            IHealth health = mutantGameObject.GetComponent<IHealth>();
            health.Current = mutantData.Hp;
            health.Max = mutantData.Hp;
            
            mutantGameObject.GetComponent<HealthController>().Construct(health);
            mutantGameObject.GetComponent<AgentMoveToPlayer>().Construct(_playerGameObject.transform);
            mutantGameObject.GetComponent<NavMeshAgent>().speed = mutantData.MoveSpeed;
            
            mutantGameObject.GetComponent<MutantAttack>()
                .Construct(mutantData.Damage, mutantData.AttackColldown, mutantData.CLeavage, mutantData.EffectiveDistance);
            
            return mutantGameObject;
        }

        public void CreateSpawner(Vector3 at)
        {
            _spawnerGameObject = Instantiate(AssetsAddress.SpawnerPrefabPath, at);
        }
        
        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            return _assets.Instantiate(prefabPath, at);
        }
        
        private GameObject Instantiate(string prefabPath)
        {
            return _assets.Instantiate(prefabPath);
        }
    }
}