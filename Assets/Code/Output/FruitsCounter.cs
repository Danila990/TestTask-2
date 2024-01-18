using Code.Enum;
using Code.Manager;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Output
{
    public class FruitsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fruitText;
        
        private int _maxFruit;
        private int _currentFruit = 0;
        private GameManager _gameManager;
        private FruitManager _fruitManager;

        [Inject]
        private void Construct(LoadSystem loadSystem, GameManager gameManager,FruitManager fruitManager)
        {
            _fruitManager = fruitManager;
            _gameManager = gameManager;
            _maxFruit = loadSystem.LevelSetting._countFruits;
        }

        private void Start()
        {
            UpdateTextFruit();
        }

        private void OnEnable()
        {
            _fruitManager.OnDeactivateFruit += UpFruit;
            _gameManager.OnRestartGame += RestartGame;
        }

        private void OnDisable()
        {
            _fruitManager.OnDeactivateFruit -= UpFruit;
            _gameManager.OnRestartGame += RestartGame;
        }

        private void UpFruit(FruitType fruitType)
        {
            _currentFruit++;
            UpdateTextFruit();
            if (_currentFruit == _maxFruit)
                _gameManager.GameComplete();
        }

        private void RestartGame()
        {
            _currentFruit = 0;
            UpdateTextFruit();
        }

        private void UpdateTextFruit() => _fruitText.text = $"{_currentFruit}/{_maxFruit}";
    }
}