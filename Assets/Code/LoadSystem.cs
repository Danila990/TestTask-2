using Code.Level.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class LoadSystem : MonoBehaviour
    {
        public LevelSetting LevelSetting;
        
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}