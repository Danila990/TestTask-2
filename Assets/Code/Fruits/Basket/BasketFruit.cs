using System.Collections.Generic;
using Code.Enum;
using Code.Generator;
using Code.Manager;
using UnityEngine;
using Zenject;

namespace Code.Fruits.Basket
{
    public class BasketFruit : MonoBehaviour
    {
        public FruitType _currentFruitType { get; private set; }
        
        private List<BasketFruitButton> _basketFruitsButton;
        private FruitGenerator _fruitGenerator;
        private FruitManager _fruitManager;

        [Inject]
        private void Construct(FruitGenerator fruitGenerator)
        {
            _fruitGenerator = fruitGenerator;
        }

        private void Start()
        {
            _basketFruitsButton = _fruitGenerator.GetBasketFruits();
            BasketFruitButtonClick(_basketFruitsButton[0]._fruitType);
        }

        public void BasketFruitButtonClick(FruitType fruitType)
        {
            foreach (BasketFruitButton fruitButton in _basketFruitsButton)
            {
                fruitButton.ChangeInteractable(true);
                
                if (fruitButton._fruitType != fruitType) continue;
                
                fruitButton.ChangeInteractable(false);
                _currentFruitType = fruitButton._fruitType;
            }
        }
    }
}