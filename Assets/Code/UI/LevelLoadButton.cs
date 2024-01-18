using Code.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class LevelLoadButton : MonoBehaviour
    {
        [SerializeField] private GameObject _lock;
        [SerializeField] private TMP_Text _levelNubmer;
        [SerializeField] private TMP_Text _priceText;
        
        private LevelButtonManager _levelButtonManager;
        private int _levelLoadIndex;
        
        private void OnEnable() => GetComponentInChildren<Button>().onClick.AddListener(ClickButton);
        private void OnDisable() => GetComponentInChildren<Button>().onClick.RemoveListener(ClickButton);

        public void Init(LevelButtonManager levelButtonManager, int levelLoadIndex, int price)
        {
            _levelButtonManager = levelButtonManager;
            _levelLoadIndex = levelLoadIndex;

            _levelNubmer.text = $"Уровень {_levelLoadIndex + 1}";
            _priceText.text = price.ToString() + "$";
        }
        
        public void OpenButton() => _lock.gameObject.SetActive(false);

        private void ClickButton() => _levelButtonManager.ButtonClick(_levelLoadIndex);
    }
}