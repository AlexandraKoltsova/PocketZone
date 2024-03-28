using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            RegisternputService();
        }
        
        private static void RegisternputService()
        {
            if (Application.isEditor)
                InputService = new KeyboardInputService();
            else
                InputService = new MobileInputService();
        }
    }
}