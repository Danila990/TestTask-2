using System;
using UnityEngine;

namespace Code.Manager
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnRestartGame;
        public event Action OnGameOver;
        public event Action OnLevelComplete;
        
        public bool _isPlayGame { get; private set; } = false;

        public void RestartGame()
        {
            StopGame();
            OnRestartGame?.Invoke();
        }

        public void GameOver()
        {
            StopGame();
            OnGameOver?.Invoke();
        }

        public void GameComplete()
        {
            StopGame();
            OnLevelComplete?.Invoke();
        }

        public void PlayGame() => _isPlayGame = true;

        public void StopGame() => _isPlayGame = false;
    }
}