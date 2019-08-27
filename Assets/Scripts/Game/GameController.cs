using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode startGameKey = KeyCode.Space;

        private PlayerDeathEventChannel playerDeathEventChannel;
        private ScoreEventChannel scoreEventChannel;
        private GameState gameState = GameState.MainMenu;

        private int score;
        public int Score => score;
        
        public event GameStateChangedEventHandler OnGameStateChanged;
        public event GameScoreChangedEventHandler OnGameScoreChanged;
        private void Awake()
        {
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            scoreEventChannel = Finder.ScoreEventChannel;

            score = 0;
        }

        private void Start()
        {
            if (!SceneManager.GetSceneByName(Scenes.GAME).isLoaded)
                StartCoroutine(LoadGame());
            else
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
        }

        private IEnumerator LoadGame()
        {
            //TODO : Show loading screen
            yield return SceneManager.LoadSceneAsync(Scenes.GAME,LoadSceneMode.Additive);
            //TODO : Hide loading screen
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
        }

        private IEnumerator UnloadGame()
        {
            yield return SceneManager.UnloadSceneAsync(Scenes.GAME);
        }

        private IEnumerator ReloadGame()
        {
            yield return UnloadGame();
            yield return LoadGame();
        }

        private void Update()
        {
            if (GameState == GameState.MainMenu && Input.GetKeyDown(startGameKey))
                GameState = GameState.Playing;
            if (GameState == GameState.GameOver && Input.GetKeyDown(startGameKey))
                RestartGame();
        }

        private void RestartGame()
        {
            GameState = GameState.MainMenu;
            score = 0;

            StartCoroutine(ReloadGame());
        }

        private void OnEnable()
        {
            playerDeathEventChannel.OnPlayerDeath += EndGame;
            scoreEventChannel.OnScored += IncrementScore;

        }

        private void OnDisable()
        {
            playerDeathEventChannel.OnPlayerDeath -= EndGame;
            scoreEventChannel.OnScored -= IncrementScore;
        }

        private void IncrementScore()
        {
            score++;
            if (OnGameScoreChanged != null) OnGameScoreChanged(score);
            //Debug.Log("score+");
        }

        private void EndGame()
        {
            gameState = GameState.GameOver;
        }

        public GameState GameState
        {
            get { return gameState; }
            private set
            {
                if (gameState != value)
                {
                    gameState = value;
                    NotifyGameStateChanged();
                }
            }
        }

        private void NotifyGameStateChanged()
        {
            if (OnGameStateChanged != null)
            {
                OnGameStateChanged(gameState);
            }
        }

    }
    public delegate void GameStateChangedEventHandler(GameState newGameState);
    public delegate void GameScoreChangedEventHandler(int newScore);

    
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
}