using Code.Manager;
using UnityEngine;
using Zenject;

namespace Code.UI.Screens
{
    public class LevelCompleteScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _miniGamePanel;
        [SerializeField] private GameObject _levelCompletePanel;
        
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Awake()
        {
            _gameManager.OnLevelComplete += LevelComplete;
            _gameManager.OnRestartGame += LevelRestart;
            LevelRestart();
        }
        
        private void OnDestroy()
        {
            _gameManager.OnLevelComplete -= LevelComplete;
            _gameManager.OnRestartGame -= LevelRestart;
        }

        private void LevelComplete()
        {
            if (Random.Range(0, 100) > 50)
                _miniGamePanel.gameObject.SetActive(true);
            else
                _levelCompletePanel.SetActive(true);
        }
        
        private void LevelRestart()
        {
            _levelCompletePanel.SetActive(false);
            _miniGamePanel.SetActive(false);
        }
    }
}