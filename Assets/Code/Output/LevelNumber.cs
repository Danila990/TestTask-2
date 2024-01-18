using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Output
{
    public class LevelNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text _leveltext;
        
        [Inject]
        private void Construct(LoadSystem loadSystem)
        {
            _leveltext.text = $"LVL {loadSystem.LevelSetting._levelNumber}";
        }
    }
}