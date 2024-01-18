using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levlCompleteCoinText;
        
        private LoadSystem _loadSystem;
        private SaveSystem _saveSystem;

        [Inject]
        private void Construct(LoadSystem loadSystem, SaveSystem saveSystem)
        {
            _loadSystem = loadSystem;
            _saveSystem = saveSystem;
        }

        private void OnEnable()
        {
            LevelCoins();
        }

        private void LevelCoins()
        {
            int completeCoins = _loadSystem.LevelSetting._completeCoins;
            _levlCompleteCoinText.text = "+" + completeCoins;
            SaveCoins(completeCoins);
        }

        private void SaveCoins(int coins)
        {
            _saveSystem.Coins += coins;
            _saveSystem.SaveAllProgress();
        }
    }
}