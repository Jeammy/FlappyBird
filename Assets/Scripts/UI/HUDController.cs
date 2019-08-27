using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class HUDController : MonoBehaviour
    {
        private Canvas canvas;
        private GameController gameController;
        private Text text;
        private void Awake()
        {
            gameController = Finder.GameController;
            canvas = GetComponent<Canvas>();
            text = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            UpdateVisibility(gameController.GameState);
            UpdateScore(gameController.Score);
        }

        private void Update()
        {
            //UpdateUI();
        }

        private void UpdateScore(int score)
        {
            text.text = score.ToString("00");
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
            gameController.OnGameScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
            gameController.OnGameScoreChanged -= UpdateScore;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.Playing;
        }
    }
}