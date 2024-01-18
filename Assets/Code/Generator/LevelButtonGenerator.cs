using Code.Manager;
using Code.UI;
using Code.UI.Setting;
using UnityEngine;
using Zenject;

namespace Code.Generator
{
    public class LevelButtonGenerator : MonoBehaviour
    {
        [SerializeField] private LevelLoadButton _prefab;
        [SerializeField] private Transform _parentSpawn;
        
        private SaveSystem _saveSystem;
        private LevelButtonManager _levelButtonManager;

        [Inject]
        private void Construct(LevelButtonManager levelButtonManager, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _levelButtonManager = levelButtonManager;
        }
        
        public LevelLoadButton[] CreateButtons(LevelButtonSetting[] levelButtonSettings)
        {
            LevelLoadButton[] buttons = new LevelLoadButton[levelButtonSettings.Length];
            bool[] levelsOpened = _saveSystem._levelsOpened;
            
            
            for (int i = 0; i < levelButtonSettings.Length; i++)
            {
                LevelLoadButton spawnButton = Instantiate(_prefab, _parentSpawn);
                buttons[i] = spawnButton;
                
                if (i < levelsOpened.Length)
                {
                    levelButtonSettings[i].IsOpened = levelsOpened[i];
                }
                else
                    levelButtonSettings[i].IsOpened = false;

                if (levelButtonSettings[i].IsOpened)
                    spawnButton.OpenButton();

                spawnButton.Init(_levelButtonManager, i, levelButtonSettings[i]._openingPrice);
            }

            return buttons;
        }
    }
}