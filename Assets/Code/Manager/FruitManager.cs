using System;
using System.Collections.Generic;
using Code.Enum;
using Code.Fruits;
using Code.Fruits.Basket;
using Code.Generator;
using Code.Player;
using UnityEngine;
using Zenject;

namespace Code.Manager
{
    public class FruitManager : MonoBehaviour
    {
        public event Action<FruitType> OnDeactivateFruit;
        
        private List<Fruit> _fruits;
        private FruitGenerator _fruitGenerator;
        private GameManager _gameManager;
        private PlayerMover _playerMover;
        private BasketFruit _basketFruit;

        [Inject]
        private void Construct(FruitGenerator fruitGenerator,GameManager gameManager, PlayerMover playerMover,
            BasketFruit basketFruit)
        {
            _fruitGenerator = fruitGenerator;
            _gameManager = gameManager;
            _playerMover = playerMover;
            _basketFruit = basketFruit;
        }
        
        private void Start()
        {
            _fruits = _fruitGenerator.GetFruits();
        }

        private void OnEnable()
        {
            _playerMover.OnTriggerCell += DeactivateFruit;
            _gameManager.OnRestartGame += RestartFruit;
        }

        private void OnDisable()
        {
            _playerMover.OnTriggerCell -= DeactivateFruit;
            _gameManager.OnRestartGame -= RestartFruit;
        }

        private void DeactivateFruit(Vector2Int cellIndex)
        {
            foreach (Fruit fruit in _fruits)
            {
                if (cellIndex == fruit._gridPosition && fruit.gameObject.activeSelf)
                {
                    if (_basketFruit._currentFruitType != fruit._type)
                    {
                        _gameManager.GameOver();
                        return;
                    }
                    
                    fruit.gameObject.SetActive(false);
                    OnDeactivateFruit?.Invoke(fruit._type);
                    return;
                }
            }
        }

        private void RestartFruit()
        {
            foreach (Fruit fruit in _fruits)
                fruit.gameObject.SetActive(true);
        }
    }
}