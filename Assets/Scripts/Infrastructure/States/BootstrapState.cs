using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Services;
using Services.Input;
using Services.Inventory;
using Services.PersistentProgress;
using Services.Randomizer;
using Services.SaveLoad;
using Services.StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private SaveLoadSystem _saveLoadSystem;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
            
            SystemsManager.Init();
            SystemsManager.Start();
        }        

        public void Enter()
        {
            _saveLoadSystem?.Load();
            _sceneLoader.Load(Boot, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
        
        private void RegisterServices()
        {
            RegisterStaticData();
            
            SystemsManager.AddInstance(GetInputSystem(), true);
            SystemsManager.Add<RandomSystem>(true);
            SystemsManager.Add<AssetProvider>(true);
            SystemsManager.Add<PersistentProgressSystem>(true);
            SystemsManager.Add<InventorySystem>(true);
            SystemsManager.Add<GameFactory>(true);
            _saveLoadSystem = (SaveLoadSystem)SystemsManager.Add<SaveLoadSystem>(true);
        }

        private void RegisterStaticData()
        {
            IStaticDataSystem staticData = new StaticDataSystem();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
        }

        private static IInputSystem GetInputSystem()
        {
            if (Application.isEditor)
                return new KeyboardInputSystem();
            else
                return new MobileInputSystem();
        }
    }
}