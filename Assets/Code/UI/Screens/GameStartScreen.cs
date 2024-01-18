using Code.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Screens
{
    public class GameStartScreen : MonoBehaviour
    {
        [SerializeField] private Button _tapToStartButton;

        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Start()
        {
            _tapToStartButton.onClick.AddListener(ScreenClick);
            _gameManager.OnRestartGame += RestartGame;

            RestartGame();
        }

        private void OnDestroy()
        {
            _tapToStartButton.onClick.RemoveListener(ScreenClick);
            _gameManager.OnRestartGame -= RestartGame;
        }

        private void ScreenClick()
        {
            _gameManager.PlayGame();
            _tapToStartButton.gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            _tapToStartButton.gameObject.SetActive(true);
        }
    }
}