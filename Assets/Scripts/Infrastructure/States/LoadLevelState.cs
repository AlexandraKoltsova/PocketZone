using Cinemachine;
using Infrastructure.Factory;
using Logic;
using Logic.Spawners;
using Player;
using Services.PersistentProgress;
using Services.StaticData;
using StaticData;
using StaticData.MutantsData;
using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string MutantSpawnerTag = "MutantSpawner";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.progressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            
            GameObject player = InitPlayer();
            InitHud(player);
            
            CameraFollow(player);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            foreach (MutantSpawnerData spawnerData in levelData.MutantSpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MutantTypeId);
            }
        }

        private GameObject InitPlayer()
        {
            return _gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
        }

        private void InitHud(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHUD();

            hud.GetComponentInChildren<HealthController>()
                .Construct(player.GetComponent<IHealth>());
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}