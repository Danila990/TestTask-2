using Code.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Fruits.Basket
{
    public class BasketFruitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public FruitType _fruitType { get; private set; }
        
        private BasketFruit _basketFruit;
        
        public void Init(FruitType fruitType, BasketFruit basketFruit, Sprite icon)
        {
            _fruitType = fruitType;
            _basketFruit = basketFruit;
            GetComponent<Image>().sprite = icon;
        }

        private void OnEnable() => _button.onClick.AddListener(ClickButton);
        private void OnDisable() => _button.onClick.RemoveListener(ClickButton);

        public void ChangeInteractable(bool state) => _button.interactable = state;
        
        private void ClickButton() => _basketFruit.BasketFruitButtonClick(_fruitType);
    }
}