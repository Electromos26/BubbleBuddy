using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
       public GameState CurrentGameState { get; private set; }

        private void Start()
        {
            CurrentGameState = GameState.MainMenu;
        }

        public void ChangeGameState(GameState state)
        {
            CurrentGameState = state;
        }
        
        
        
    }

    public enum GameState
    {
        MainMenu,
        GameStarting,
        BetweenWaves,
        InBattle,
        GameOver
    }
}
