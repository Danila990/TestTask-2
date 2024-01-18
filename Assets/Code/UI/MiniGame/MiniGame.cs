using System.Collections.Generic;
using Code.Generator;
using Code.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.MiniGame
{
    public class MiniGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text _completeText;
        [SerializeField] private TMP_Text _bonusCoinsText;
        [SerializeField] private Button _closeMiniGameButton;
        [SerializeField] private List<MiniGameImage> _buttonImage;
        
        private List<Sprite> _allImageTypes = new List<Sprite>();
        private List<Image> _resultImages = new List<Image>();
        
        private SaveSystem _saveSystem;
        private FruitGenerator _fruitGenerator;

        [Inject]
        private void Construct(SaveSystem saveSystem, FruitGenerator fruitGenerator)
        {
            _saveSystem = saveSystem;
            _fruitGenerator = fruitGenerator;
        }
        
        private void Awake() => _allImageTypes = _fruitGenerator.GetFruitSprite();

        private void OnEnable()
        {
            _completeText.gameObject.SetActive(false);
            _bonusCoinsText.gameObject.SetActive(false);
            _closeMiniGameButton.gameObject.SetActive(false);
            _resultImages = new List<Image>();

            SetupImageButtons();
        }
        
        public void ButtonImageClick(MiniGameImage miniGameImage)
        {
            if (CheckDuplicate(miniGameImage._image) || _resultImages.Count > 3) return;

            miniGameImage.OpenSprite();
            _resultImages.Add(miniGameImage._image);

            if (_resultImages.Count == 3)
            {
                if (CheckResult())
                {
                    int randomCoin = Random.Range(50, 100);
                    _bonusCoinsText.gameObject.SetActive(true);
                    _bonusCoinsText.text = "+" + randomCoin;
                    _completeText.color = Color.green;
                    _completeText.text = "Complete";
                    _saveSystem.Coins += randomCoin;
                    _saveSystem.SaveAllProgress();
                }
                else
                {
                    _completeText.color = Color.red;
                    _completeText.text = "Loss";
                }
                
                _completeText.gameObject.SetActive(true);
                _closeMiniGameButton.gameObject.SetActive(true);
            }
        }

        private void SetupImageButtons()
        {
            List<Sprite> randomSprite = GenerateRandomSprite();

            for (int i = 0; i < _buttonImage.Count; i++)
                _buttonImage[i].SetupOpenSprite(randomSprite[i]);
        }
        
        private bool CheckResult()
        {
            for (int i = 1; i < _resultImages.Count; i++)
            {
                if (_resultImages[0].sprite != _resultImages[i].sprite)
                    return false;
            }

            return true;
        }
        
        private bool CheckDuplicate(Image findImage)
        {
            foreach (Image image in _resultImages)
            {
                if (image == findImage)
                    return true;
            }

            return false;
        }
        
        private List<Sprite> GenerateRandomSprite()
        {
            List<Sprite> imageTypes = new List<Sprite>();
            
            for (int i = 0; i < 3; i++)
                imageTypes.AddRange(_allImageTypes);
            
            for (int i = 0; i < imageTypes.Count; i++)
            {
                Sprite firstSprite = imageTypes[i];
                int randomIndex = Random.Range(i, imageTypes.Count);
                imageTypes[i] = imageTypes[randomIndex];
                imageTypes[randomIndex] = firstSprite;
            }

            return imageTypes;
        }
    }
}