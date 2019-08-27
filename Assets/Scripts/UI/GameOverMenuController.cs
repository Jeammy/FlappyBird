using UnityEngine;

namespace Game
{
    public sealed class GameOverMenuController : MonoBehaviour
    {
        private Canvas canvas;
        private GameController gameController;
        private void Awake()
        {
            gameController = Finder.GameController;
            canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            UpdateVisibility(gameController.GameState);
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.GameOver;
        }
    }
}