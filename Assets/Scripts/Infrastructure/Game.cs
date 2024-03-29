using Infrastructure.States;
using Logic;
using Services;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}