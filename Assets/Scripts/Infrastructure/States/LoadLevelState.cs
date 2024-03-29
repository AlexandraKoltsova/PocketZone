using Cinemachine;
using Infrastructure.Factory;
using Logic;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayerPrefabPath = "Units/Player/Player";
        private const string HUDPrefabPath = "UI/HUD";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreateHero(at: initialPoint);

            _gameFactory.CreateHud();
            
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}