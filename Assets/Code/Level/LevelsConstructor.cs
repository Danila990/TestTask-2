using Code.Level.Settings;
using UnityEngine;

namespace Code.Level
{
    public class LevelsConstructor : MonoBehaviour
    {
        [SerializeField] private LevelSetting[] _levelSettings;

        public LevelSetting GetLevelSetting(int indexLevel) => _levelSettings[indexLevel];
    }
}