using Data;
using Services.PersistentProgress;
using Services.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressSystem _progressSystem;
        private readonly ISaveLoadSystem _saveLoadSystem;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressSystem progressSystem, ISaveLoadSystem saveLoadSystem)
        {
            _stateMachine = stateMachine;
            _progressSystem = progressSystem;
            _saveLoadSystem = saveLoadSystem;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressSystem.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }
        
        private void LoadProgressOrInitNew()
        {
            _progressSystem.Progress = _saveLoadSystem.LoadPlayerProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(initialLevel: "Main");

            playerProgress.Health.MaxHP = 50;
            playerProgress.Health.ResetHP();

            playerProgress.Stats.Damage = 10f;
            playerProgress.Stats.Speed = 15f;
            playerProgress.Stats.DamageRadius = 4.5f;
            
            return playerProgress;
        }
    }
}