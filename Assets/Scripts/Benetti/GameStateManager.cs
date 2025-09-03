using UnityEngine;
using UnityEngine.SceneManagement;

namespace Benetti
{
    public static class GameStateManager
    {
        public enum GameState
        {
            MainMenu,
            InGame,
            Paused,
            Options,
            Loading
        }
        
        public static Scene currentScene;
        static void Awake()
        {
            currentScene = SceneManager.GetActiveScene();
            switch (currentScene.name)
            {
                case "MainMenu":
                    SetState(GameState.MainMenu);
                    break;
                case "PrototypingScene":
                    SetState(GameState.InGame);
                    break;
            }
        }
        public static GameState CurrentState { get; private set; } = GameState.MainMenu;
        
        // <summary>
        /// Postavlja novo stanje igre i automatski prilagođava kursor.
        /// </summary>
        public static void SetState(GameState newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                case GameState.Paused:
                    Time.timeScale = 0f; // Zaustavi igru
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                case GameState.Options:
                    Time.timeScale = 0f; // Zaustavi igru
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;

                case GameState.InGame:
                    Time.timeScale = 1f; // Nastavi igru
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;

                case GameState.Loading:
                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }

            Debug.Log($"Game state changed to: {newState}");
        }
        
        /// <summary>
        /// Provjera da li je igra pauzirana.
        /// </summary>
        public static bool IsPaused => CurrentState == GameState.Paused;

        /// <summary>
        /// Provjera da li smo u main meniu.
        /// </summary>
        public static bool IsInMainMenu => CurrentState == GameState.MainMenu;

        /// <summary>
        /// Provjera da li smo u gameplayu.
        /// </summary>
        public static bool IsInGameplay => CurrentState == GameState.InGame;
    }
    
}

