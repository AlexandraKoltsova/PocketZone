using Cinemachine;
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
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
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
            
            GameObject player = Instantiate(PlayerPrefabPath, at: initialPoint.transform.position);
            Instantiate(HUDPrefabPath);
            
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }


        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}