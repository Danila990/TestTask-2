using System;
using System.IO;
using UnityEngine;

namespace Code
{
    public class SaveSystem : MonoBehaviour
    {
        [HideInInspector] public int Coins;
        [HideInInspector] public bool[] _levelsOpened;
        
        private string _savePath;
        private string _saveFileName = "saveData.json";

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
           _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
            _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif
            LoadProgress();
        }
        
        public void SaveAllProgress()
        {
            SaveData saveData = new SaveData()
            {
                Coins = this.Coins,
                LevelsOpened = _levelsOpened,
            };

            string json = JsonUtility.ToJson(saveData, true);

            try
            {
                File.WriteAllText(_savePath,json);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private void LoadProgress()
        {
            if (!File.Exists(_savePath))
            {
                Coins = 0;
                _levelsOpened = new[] { true };
                return;
            }

            string json = File.ReadAllText(_savePath);

            try
            {
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);
                Coins = saveData.Coins;
                _levelsOpened = saveData.LevelsOpened;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
    
    [Serializable]
    public struct SaveData
    {
        public int Coins;
        public bool[] LevelsOpened;
    }
}