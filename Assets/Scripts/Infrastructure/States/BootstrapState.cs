using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Services;
using Services.Input;
using Services.Inventory;
using Services.PersistentProgress;
using Services.Randomizer;
using Services.SaveLoad;
using Services.Spawner;
using Services.StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        private const string Main = "Main";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private SaveLoadSystem _saveLoadSystem;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            
            RegisterSystems();
            
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
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        public void Exit()
        {
        }
        
        private void RegisterSystems()
        {
            SystemsManager.AddInstance(GetInputSystem(), true);
            SystemsManager.Add<RandomSystem>(true);
            SystemsManager.Add<AssetProvider>(true);
            SystemsManager.Add<PersistentProgressSystem>(true);
            SystemsManager.Add<InventorySystem>(true);
            
            IStaticDataSystem staticData = (StaticDataSystem)SystemsManager.Add<StaticDataSystem>(true);
            staticData.LoadConfigs();
            
            SystemsManager.Add<GameFactory>(true);
            _saveLoadSystem = (SaveLoadSystem)SystemsManager.Add<SaveLoadSystem>(true);
            
            SystemsManager.Add<PlayerSpawnSystem>(true);
            SystemsManager.Add<HUDSpawnSystem>(true);
            SystemsManager.Add<MutantSpawnSystem>(true);
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