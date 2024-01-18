using Code.Manager;
using UnityEngine;
using Zenject;

namespace Code.UI.Screens
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        
        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Start()
        {
            _gameManager.OnRestartGame += RestartGame;
            _gameManager.OnGameOver += GameOver;

            RestartGame();
        }

        private void OnDestroy()
        {
            _gameManager.OnRestartGame -= RestartGame;
            _gameManager.OnGameOver -= GameOver;
        }

        private void RestartGame()
        {
            _gameOverPanel.SetActive(false);
        }

        private void GameOver()
        {
            _gameOverPanel.SetActive(true);
        }
    }
}