using Infrastructure.States;
using Logic;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}