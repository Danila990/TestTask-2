using Code.Generator;
using Code.Level;
using Code.Systems;
using Code.UI;
using Code.UI.Setting;
using UnityEngine;
using Zenject;

namespace Code.Manager
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private LevelButtonSetting[] _levelButtonSetting;
        
        private LevelButtonGenerator _levelButtonGenerator;
        private LevelLoadButton[] _buttons;
        private Coins _coins;
        private LoadSystem _loadSystem;
        private SaveSystem _saveSystem;
        private LevelsConstructor _levelsConstructor;

        [Inject]
        private void Construct(LevelButtonGenerator levelButtonGenerator, Coins coins
            ,LoadSystem loadSystem, SaveSystem saveSystem, LevelsConstructor levelsConstructor)
        {
            _levelsConstructor = levelsConstructor;
            _saveSystem = saveSystem;
            _loadSystem = loadSystem;
            _coins = coins;
            _levelButtonGenerator = levelButtonGenerator;
        }
        
        private void Start()
        {
            _buttons = new LevelLoadButton[_levelButtonSetting.Length];
            _buttons = _levelButtonGenerator.CreateButtons(_levelButtonSetting);
        }

        public void ButtonClick(int index)
        {
            if(_levelButtonSetting[index].IsOpened == false)
                if (_coins._currentCoin >= _levelButtonSetting[index]._openingPrice)
                {
                    _levelButtonSetting[index].IsOpened = true;
                    _coins.ChangeCoin(-_levelButtonSetting[index]._openingPrice);
                    _buttons[index].OpenButton();
                    SaveLevels();
                    return;
                }
                else return;
                
            _loadSystem.LevelSetting = _levelsConstructor.GetLevelSetting(index);
            _loadSystem.LoadGame();
        }
        
        private void SaveLevels()
        {
            bool[] levelsOpened = new bool[_buttons.Length];
            for (int i = 0; i < _levelButtonSetting.Length; i++)
            {
                levelsOpened[i] = _levelButtonSetting[i].IsOpened;
            }

            _saveSystem._levelsOpened = levelsOpened;
            _saveSystem.SaveAllProgress();
        }
    }
}