using System.Collections;
using Code.Manager;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Output
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        
        private float _timeRemaining = 60f;
        private float _startRemainingTime;
        private GameManager _gameManager;
        
        [Inject]
        private void Construct(LoadSystem loadSystem, GameManager gameManager)
        {
            _gameManager = gameManager;
            _startRemainingTime = loadSystem.LevelSetting._timer;
        }

        private void Start()
        {
            RestartTimer();
        }

        private void OnEnable()
        {
            _gameManager.OnRestartGame += RestartTimer;
            _gameManager.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            _gameManager.OnRestartGame -= RestartTimer;
            _gameManager.OnGameOver -= GameOver;
        }

        private void RestartTimer()
        {
            GameOver();
            _timeRemaining = _startRemainingTime;
            UpdateTimerText();
            
            StartCoroutine(Timer());
        }

        private void GameOver() => StopAllCoroutines();
        
        private IEnumerator Timer()
        {
            while (_timeRemaining > 0)
            {
                yield return new WaitUntil(() => _gameManager._isPlayGame);
                yield return new WaitForSeconds(1f);
                _timeRemaining--;
                UpdateTimerText();
            }
            
            UpdateTimerText();
            _gameManager.GameOver();
        }

        private void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(_timeRemaining / 60);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60);
            _timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}