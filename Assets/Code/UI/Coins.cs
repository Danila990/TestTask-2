using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class Coins : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinText;
        
        public int _currentCoin { get; private set; }
        
        private SaveSystem _saveSystem;

        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        
        private void Start()
        {
            _currentCoin = _saveSystem.Coins;
            OutputCoin();
        }

        public void ChangeCoin(int coinValue)
        {
            if (coinValue + _currentCoin < 0)
            {
                Debug.LogError("coinValue < 0");
                return;
            }

            _currentCoin += coinValue;
            OutputCoin();
            _saveSystem.Coins = _currentCoin;
            _saveSystem.SaveAllProgress();
        }

        private void OutputCoin() => _coinText.text = _currentCoin.ToString();
    }
}