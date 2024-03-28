using Services.Input;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }        

        public void Enter()
        {
            _sceneLoader.Load(Boot, onLoaded: EnterLoadLevel);
            RegisterServices();
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        public void Exit()
        {
        }
        
        private void RegisterServices()
        {
            Game.InputService = InputService();
        }
        
        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new KeyboardInputService();
            else
                return new MobileInputService();
        }
    }
}